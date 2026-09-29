using Features.Interaction.UseCases.Ports;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Rig
{
    public sealed class InteractionRootBehaviour : MonoBehaviour, IInteractionRoot
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void SetPosition(Vector3 position) => transform.position = position;
        public void SetRotation(Quaternion rotation) => transform.rotation = rotation;
        public void SetVelocity(Vector3 velocity) => _rb.linearVelocity = velocity;
        public void SetAngularVelocity(Vector3 angularVelocity) => _rb.angularVelocity = angularVelocity;
    }
}
