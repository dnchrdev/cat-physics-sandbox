using System;
using Features.Interaction.Entities.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.Entities
{
    public sealed class InteractableEntity
    {
        private const float GrabRayDistance = 100f;

        public event Action Focused;
        public event Action Unfocused;
        public event Action Grabbed;
        public event Action Released;
        public event Action Threw;
        public event Action Hit;
        public event Action ColliderHit;

        private readonly InteractableSettings _settings;
        private readonly IInteractableBody _body;
        private readonly IGrabJoint _joint;

        private IEntity _owner;
        private Vector3 _savedPosition;
        private Quaternion _savedRotation;

        public InteractableState State { get; private set; }
        public bool IsGrabbed => State == InteractableState.Grabbed;
        public bool IsRagdoll => _settings.IsRagdoll;
        public bool ShowsGrabPoint => _settings.ShowsGrabPoint;
        public InteractionType InteractionType => _settings.InteractionType;
        public Vector3 HandAnchorDirection => _settings.HandAnchorDirection;
        public Vector3 Position => _body.Position;

        private bool AnchorsToGrabPoint => _settings.GrabType == GrabType.NearestPoint;
        private bool DrivesRotation => _settings.GrabType == GrabType.CenterWithRotation;

        public InteractableEntity(InteractableSettings settings, IInteractableBody body, IGrabJoint joint)
        {
            _settings = settings;
            _body = body;
            _joint = joint;

            _body.IsKinematic = settings.IsKinematicOnStart;
            ApplyPhysicsMode(false);
        }

        public void Focus()
        {
            if (State != InteractableState.Idle) return;
            State = InteractableState.Focused;
            Focused?.Invoke();
        }

        public void Unfocus()
        {
            if (State != InteractableState.Focused) return;
            State = InteractableState.Idle;
            Unfocused?.Invoke();
        }

        public bool TryGrab(IEntity owner, Vector3 grabPoint)
        {
            if (!_settings.CanGrab || IsGrabbed) return false;

            _body.IsKinematic = false;
            var localAnchor = AnchorsToGrabPoint ? ToLocal(grabPoint) : Vector3.zero;
            _joint.Attach(localAnchor, DrivesRotation);

            _owner = owner;
            State = InteractableState.Grabbed;
            Grabbed?.Invoke();
            ApplyPhysicsMode(true);
            return true;
        }
        
        public Vector3 GetGrabPoint(Vector3 handPosition, Vector3 viewOrigin, Vector3 viewDirection)
        {
            if (!AnchorsToGrabPoint) return _body.Position;

            return _body.Raycast(viewOrigin, viewDirection, GrabRayDistance, out var pointUnderView)
                ? pointUnderView
                : _body.ClosestPoint(handPosition);
        }

        public void MoveGrabAnchor(Vector3 grabPoint)
        {
            if (!IsGrabbed || !AnchorsToGrabPoint) return;
            _joint.MoveAnchor(ToLocal(grabPoint));
        }

        public float GetHoldDistance(Vector3 viewerPosition, float minGap)
        {
            if (_settings.UseCustomHandDistance) return _settings.CustomHandDistance;

            var closestToViewer = _body.ClosestPoint(viewerPosition);
            return minGap + Vector3.Distance(closestToViewer, _body.Position);
        }

        public void Throw(Vector3 toPoint, float power)
        {
            State = InteractableState.Idle;
            Threw?.Invoke();
            ApplyPhysicsMode(false);
            _joint.Detach();

            var direction = (toPoint - _body.Position).normalized;
            _body.AddImpulse(direction * power);
        }

        public void Release()
        {
            State = InteractableState.Idle;
            Released?.Invoke();
            ApplyPhysicsMode(held: false);
            _joint.Detach();
        }

        public void HitWith(Vector3 direction, Vector3 atPoint, float power, IEntity owner)
        {
            Hit?.Invoke();
            _owner = owner;
            if (!_settings.CanBeHit) return;

            _body.IsKinematic = false;
            _body.AddImpulseAtPoint(direction * power, atPoint);
        }
        
        public void HandleCollision(ITarget other, float impactSpeed)
        {
            ColliderHit?.Invoke();
            if (IsGrabbed) return;

            var owner = TakeOwner();
            if (!_settings.DamagesOnCollision || owner == null) return;

            other?.ReceiveHit(new AttackInfo(owner, false, impactSpeed));
        }

        public void HandleTrigger(ITarget other)
        {
            if (IsGrabbed) return;

            var owner = TakeOwner();
            if (!_settings.DamagesOnTrigger || owner == null) return;

            other?.ReceiveHit(new AttackInfo(owner, true));
        }

        public void SaveState()
        {
            _savedPosition = _body.Position;
            _savedRotation = _body.Rotation;
        }

        public void ResetState()
        {
            _body.ResetMotion(_settings.IsKinematicOnStart);
            ApplyPhysicsMode(held: false);
            _body.SetPose(_savedPosition, _savedRotation);
        }

        private IEntity TakeOwner()
        {
            var owner = _owner;
            _owner = null;
            return owner;
        }

        private void ApplyPhysicsMode(bool held)
        {
            if (_settings.IsRagdoll) return;
            _body.SetHeldMode(held);
        }

        private Vector3 ToLocal(Vector3 worldPoint) =>
            Quaternion.Inverse(_body.Rotation) * (worldPoint - _body.Position);
    }
}
