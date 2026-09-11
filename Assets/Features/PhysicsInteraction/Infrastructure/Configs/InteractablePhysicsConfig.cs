using Features.PhysicsInteraction.Infrastructure.Data;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "InteractablePhysicsConfig", menuName = "Configs/InteractablePhysicsConfig")]
    public class InteractablePhysicsConfig : ScriptableObject
    {
        [field: Header("Default")]
        [field: SerializeField] public PhysicsMaterial DefaultMaterial { get; private set; }
        [field: SerializeField] public ColliderDrag DefaultDrag { get; private set; }

        [field: Header("Interact")]
        [field: SerializeField] public PhysicsMaterial InteractMaterial { get; private set; }
        [field: SerializeField] public ColliderDrag InteractDrag { get; private set; }
    }
}