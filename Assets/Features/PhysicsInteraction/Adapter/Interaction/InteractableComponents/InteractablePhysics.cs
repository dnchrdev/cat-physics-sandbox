using Feature.PhysicsInteraction;
using UnityEngine;

namespace Feature.PhysicsInteractio
{
    public class InteractablePhysics
    {
        private readonly Rigidbody _rb;
        private readonly Collider _collider;
        private readonly InteractablePhysicsConfig _interactablePhysicsConfig;
        private bool _isRagdoll;
        

        public InteractablePhysics(
            Rigidbody rb,
            Collider collider,
            InteractablePhysicsConfig interactablePhysicsConfig,
            bool isKinematicOnStart,
            bool isRagdoll)
        {
            _rb = rb;
            _collider = collider;
            _interactablePhysicsConfig = interactablePhysicsConfig;
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
            _rb.linearDamping = _interactablePhysicsConfig.InteractDrag.Drag;
            _rb.angularDamping = _interactablePhysicsConfig.InteractDrag.AngularDrag;

            if (_collider != null)
            {
                _collider.material = _interactablePhysicsConfig.InteractMaterial;
            }
        }

        public void SetDefaultPhysics()
        {
            if (_isRagdoll) return;

            _rb.interpolation = RigidbodyInterpolation.None;
            _rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            _rb.linearDamping = _interactablePhysicsConfig.DefaultDrag.Drag;
            _rb.angularDamping = _interactablePhysicsConfig.DefaultDrag.AngularDrag;

            if (_collider != null)
            {
                _collider.material = _interactablePhysicsConfig.DefaultMaterial;
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