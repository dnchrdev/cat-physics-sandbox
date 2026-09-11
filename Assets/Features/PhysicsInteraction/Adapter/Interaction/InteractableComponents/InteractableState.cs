using UnityEngine;

namespace Features.PhysicsInteraction.Adapter.Interaction.InteractableComponents
{
    public class InteractableState
    {
        private readonly Transform _transform;
        private readonly InteractablePhysics _physics;
        private readonly bool _isKinematicOnStart;

        private Vector3 _savedPosition;
        private Quaternion _savedRotation;

        public InteractableState(Transform transform,InteractablePhysics physics, bool isKinematicOnStart)
        {
            _transform = transform;
            _physics = physics;
            _isKinematicOnStart = isKinematicOnStart;
        }

        public void SaveState()
        {
            _savedPosition = _transform.position;
            _savedRotation = _transform.rotation;
        }

        public void ResetState()
        {
            _transform.position = _savedPosition;
            _transform.rotation = _savedRotation;
            _physics.ResetPhysics(_isKinematicOnStart);
        }
    }
}