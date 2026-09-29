using UnityEngine;

namespace Features.Interaction.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "InteractableMaterialConfig", menuName = "Configs/InteractableMaterialConfig")]
    public sealed class InteractableVisualConfig : ScriptableObject
    {
        [field: SerializeField] public bool UseGrabPositionIndication { get; private set; }

        [field: SerializeField] public bool UseOutlineOnFocus { get; private set; }
        [field: SerializeField] public OutlineMode OutlineModeOnFocus { get; private set; } = OutlineMode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnFocus { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnFocus { get; private set; } = 5f;

        [field: SerializeField] public Material OnGrabMaterial { get; private set; }
        [field: SerializeField] public bool UseOutlineOnGrab { get; private set; }
        [field: SerializeField] public OutlineMode OutlineModeOnGrab { get; private set; } = OutlineMode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnGrab { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnGrab { get; private set; } = 5f;
    }
}
