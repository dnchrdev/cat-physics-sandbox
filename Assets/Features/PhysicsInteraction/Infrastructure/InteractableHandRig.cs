using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure
{
    public class InteractableHandRig : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody HandRB { get; private set; }
        [field: SerializeField] public ConfigurableJoint HandJoint { get; private set; }
    }
}