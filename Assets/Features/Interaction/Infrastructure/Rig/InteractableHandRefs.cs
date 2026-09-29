using Features.Interaction.UseCases.Ports;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Rig
{
    public sealed class InteractableHandRefs : MonoBehaviour, IHand
    {
        [field: SerializeField] public Rigidbody HandRigidbody { get; private set; }
        [field: SerializeField] public ConfigurableJoint HandJoint { get; private set; }

        public Vector3 Position => HandRigidbody.position;

        public void SetHoldAnchor(Vector3 direction, float distance)
        {
            HandJoint.connectedAnchor = direction.normalized * distance;
        }
    }
}
