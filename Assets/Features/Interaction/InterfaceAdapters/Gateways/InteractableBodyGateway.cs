using Features.Interaction.Entities.Ports;
using Features.Interaction.Infrastructure.Configs;
using UnityEngine;

namespace Features.Interaction.InterfaceAdapters.Gateways
{
    public sealed class InteractableBodyGateway : IInteractableBody
    {
        private readonly Rigidbody _rigidbody;
        private readonly Collider _collider;
        private readonly InteractablePhysicsConfig _physics;

        public InteractableBodyGateway(Rigidbody rigidbody, Collider collider, InteractablePhysicsConfig physics)
        {
            _rigidbody = rigidbody;
            _collider = collider;
            _physics = physics;
        }

        public Vector3 Position => _rigidbody != null ? _rigidbody.position : Vector3.zero;
        public Quaternion Rotation => _rigidbody != null ? _rigidbody.rotation : Quaternion.identity;

        public bool IsKinematic
        {
            set
            {
                if (_rigidbody != null) _rigidbody.isKinematic = value;
            }
        }

        public void SetHeldMode(bool held)
        {
            if (_rigidbody == null) return;

            SetSmoothMotion(held);

            if (_physics == null) return;
            _rigidbody.linearDamping = held ? _physics.InteractDrag : _physics.DefaultDrag;
            _rigidbody.angularDamping = held ? _physics.InteractAngularDrag : _physics.DefaultAngularDrag;

            var material = held ? _physics.InteractMaterial : _physics.DefaultMaterial;
            if (_collider != null && material != null)
                _collider.material = material;
        }

        public void ResetMotion(bool isKinematic)
        {
            if (_rigidbody == null) return;

            _rigidbody.isKinematic = true;
            SetSmoothMotion(false);
            _rigidbody.isKinematic = isKinematic;
        }

        public void SetPose(Vector3 position, Quaternion rotation)
        {
            if (_rigidbody == null) return;

            _rigidbody.position = position;
            _rigidbody.rotation = rotation;
        }

        public void AddImpulse(Vector3 impulse)
        {
            if (_rigidbody != null) _rigidbody.AddForce(impulse, ForceMode.Impulse);
        }

        public void AddImpulseAtPoint(Vector3 impulse, Vector3 point)
        {
            if (_rigidbody != null) _rigidbody.AddForceAtPosition(impulse, point, ForceMode.Impulse);
        }

        public Vector3 ClosestPoint(Vector3 from)
        {
            if (_collider == null) return from;

            var colliderTransform = _collider.transform;
            return Physics.ClosestPoint(from, _collider, colliderTransform.position, colliderTransform.rotation);
        }

        public bool Raycast(Vector3 origin, Vector3 direction, float maxDistance, out Vector3 point)
        {
            point = default;
            if (_collider == null || !_collider.Raycast(new Ray(origin, direction), out var hit, maxDistance))
                return false;

            point = hit.point;
            return true;
        }

        private void SetSmoothMotion(bool smooth)
        {
            _rigidbody.interpolation = smooth ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None;
            _rigidbody.collisionDetectionMode = smooth
                ? CollisionDetectionMode.ContinuousDynamic
                : CollisionDetectionMode.Discrete;
        }
    }
}
