using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public enum Mode
    {
        OutlineAll,
        OutlineVisible,
        OutlineHidden,
        OutlineAndSilhouette,
        SilhouetteOnly
    }

    [CreateAssetMenu(fileName = "InteractableMaterialConfig", menuName = "SO/InteractableMaterialConfig")]
    public class VisualConfig : ScriptableObject
    {
        [field: SerializeField] public bool UseGrabPositionIndication { get; private set; }
        [field: SerializeField] public bool UseOutlineOnFocus { get; private set; } = false;
        [field: SerializeField] public Mode OutlineModeOnFocus { get; private set; } = Mode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnFocus { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnFocus { get; private set; } = 5f;
        [field: SerializeField] public Material OnGrabMaterial { get; private set; }
        [field: SerializeField] public bool UseOutlineOnGrab { get; private set; } = false;
        [field: SerializeField] public Mode OutlineModeOnGrab { get; private set; } = Mode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnGrab { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnGrab { get; private set; } = 5f;
    }
}