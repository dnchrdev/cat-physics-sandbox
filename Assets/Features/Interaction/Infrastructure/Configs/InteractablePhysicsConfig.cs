using UnityEngine;

namespace Features.Interaction.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "InteractablePhysicsConfig", menuName = "Configs/InteractablePhysicsConfig")]
    public sealed class InteractablePhysicsConfig : ScriptableObject
    {
        [field: Header("Default")]
        [field: SerializeField] public PhysicsMaterial DefaultMaterial { get; private set; }
        [field: SerializeField] public float DefaultDrag { get; private set; }
        [field: SerializeField] public float DefaultAngularDrag { get; private set; }

        [field: Header("Interact")]
        [field: SerializeField] public PhysicsMaterial InteractMaterial { get; private set; }
        [field: SerializeField] public float InteractDrag { get; private set; }
        [field: SerializeField] public float InteractAngularDrag { get; private set; }
    }
}
