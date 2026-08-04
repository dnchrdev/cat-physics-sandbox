using System;
using Feature.CameraFeature;
using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class GrabbedInteractableHandler
    {
        private readonly IInteractionTipShower _tipsShower;
        private readonly InteractableHandRig _handRig;
        private readonly IReadOnlyCamera _camera;
        private readonly InteractionControllerConfig _config;
        private readonly InteractionIndication _indication;
        private readonly InteractionDetector _detector;

        private Interactable _grabbed;
        private Collider _grabbedCollider;
        private Vector3 _grabPosition;
        private float _timeSinceGrabbed;

        public bool HasGrabbed => _grabbed != null;

        public GrabbedInteractableHandler(
            IInteractionTipShower tipsShower,
            InteractableHandRig handRig,
            IReadOnlyCamera camera,
            InteractionControllerConfig config,
            InteractionIndication indication,
            InteractionDetector detector)
        {
            _tipsShower = tipsShower;
            _handRig = handRig;
            _camera = camera;
            _config = config;
            _indication = indication;
            _detector = detector;   
        }

        public void Tick(float dt)
        {
            _grabPosition = _grabbed.GetGrabPosition(_handRig.HandRB.position,  _camera, _config.InteractionMask);
            _grabbed.UpdateInteractableAnchor(_grabPosition);
            
            if (_grabbed.UseGrabPositionIndication)
            {
                _indication.UpdateGrabIndicationPosition(_grabPosition);
            }

            _timeSinceGrabbed += Time.deltaTime;
            _handRig.HandJoint.connectedAnchor = _grabbed.Anchor.normalized * GetHandAnchorDistance();
        }

        
        public void ShowVisualOnGrab()
        {
            if (_grabbed.UseGrabPositionIndication)
            {
                _indication.ShowGrabIndication(_grabPosition);
                _indication.SwitchGrabIndicationImage(false);
            }
            
            _grabbed.ShowGrabVisual();
        }
        
        public bool TryGrab(Interactable target, Collider targetCollider, IEntity owner)
        {
            if(target == null || targetCollider == null || owner == null) return false;
            
            if (_grabbed != null)
                TryRelease(true);

            bool success = target.TryGrab(_handRig.HandRB, owner, _grabPosition);
            if (!success) return false;

            _grabbed = target;
            _grabbedCollider = targetCollider;
            _timeSinceGrabbed = 0f;
  
            return true;
        }
        
        public bool ShouldRelease()
        {
            bool tooFar = Vector3.Distance(
                _grabbed.transform.position,
                _handRig.HandRB.position) > _config.ForceGrabbedReleaseDistance;

            return tooFar || !_grabbed.IsGrabbed;
        }

        public void Throw()
        {
            if (_grabbed == null) return;

            var aimingPoint = _detector.GetThrowAimingPoint(_grabbedCollider);
            
            _grabbed.Throw(aimingPoint, _config.GrabbedThrowPower);
            ClearGrabbedData();
        }

        public void TryRelease(bool forced)
        {
            if(_grabbed == null) return;
            
            if (forced == false && _timeSinceGrabbed < _config.ReleaseCooldown) return;
            
            _grabbed.Release();
            ClearGrabbedData();
        }

        private void ClearGrabbedData()
        {
            _grabbed = null;
            _grabbedCollider = null;
            _tipsShower.CloseAllTips();
        }
        
        private float GetHandAnchorDistance()
        {
            if (_grabbed.UseCustomHandDistance)
            {
                return _grabbed.CustomHandDistance;
            }

            var cameraPos = _camera.Position;
            var grabPos = _grabbed.transform.position;

            var closestPoint = Physics.ClosestPoint(cameraPos, _grabbedCollider, grabPos, _grabbed.transform.rotation);

            return _config.MinDistanceBetweenPlayerAndInteractableMesh + Vector3.Distance(closestPoint, grabPos);
        }

    }
}