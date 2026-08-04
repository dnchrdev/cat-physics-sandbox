using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractableHandRig : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody HandRB { get; private set; }
        [field: SerializeField] public ConfigurableJoint HandJoint { get; private set; }
    }
}