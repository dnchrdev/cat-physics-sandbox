using Feature.PhysicsInteraction;
using UnityEngine;

namespace Feature.PhysicsInteractio
{
    public class InteractablePhysics
    {
        private readonly Rigidbody _rb;
        private readonly Collider _collider;
        private readonly PhysicsConfig _physicsConfig;
        private bool _isRagdoll;
        

        public InteractablePhysics(
            Rigidbody rb,
            Collider collider,
            PhysicsConfig physicsConfig,
            bool isKinematicOnStart,
            bool isRagdoll)
        {
            _rb = rb;
            _collider = collider;
            _physicsConfig = physicsConfig;
            _isRagdoll = isRagdoll;

            if (_rb == null) return;

            _rb.isKinematic = isKinematicOnStart;
            SetDefaultPhysics();
        }

        public void SetGrabPhysics()
        {
            if (_isRagdoll) return;

            _rb.interpolation = RigidbodyInterpolation.Interpolate;
            _rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            _rb.linearDamping = _physicsConfig.InteractDrag.Drag;
            _rb.angularDamping = _physicsConfig.InteractDrag.AngularDrag;

            if (_collider != null)
            {
                _collider.material = _physicsConfig.InteractMaterial;
            }
        }

        public void SetDefaultPhysics()
        {
            if (_isRagdoll) return;

            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _rb.linearDamping = _physicsConfig.DefaultDrag.Drag;
            _rb.angularDamping = _physicsConfig.DefaultDrag.AngularDrag;

            if (_collider != null)
            {
                _collider.material = _physicsConfig.DefaultMaterial;
            }
        }

        public void ResetPhysics(bool isKinematicOnStart)
        {
            _rb.isKinematic = true;
            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;

            _rb.isKinematic = isKinematicOnStart;

            SetDefaultPhysics();
        }
    }
}