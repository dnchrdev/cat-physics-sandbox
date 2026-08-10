using System;
using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [CreateAssetMenu(fileName = "InteractableMaterialConfig", menuName = "Configs/InteractableMaterialConfig")]
    public class InteractableVisualConfig : ScriptableObject
    {
        [field: SerializeField] public bool UseGrabPositionIndication { get; private set; }
        [field: SerializeField] public bool UseOutlineOnFocus { get; private set; } = false;
        [field: SerializeField] public OutlineMode OutlineOutlineModeOnFocus { get; private set; } = OutlineMode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnFocus { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnFocus { get; private set; } = 5f;
        [field: SerializeField] public Material OnGrabMaterial { get; private set; }
        [field: SerializeField] public bool UseOutlineOnGrab { get; private set; } = false;
        [field: SerializeField] public OutlineMode OutlineOutlineModeOnGrab { get; private set; } = OutlineMode.OutlineVisible;
        [field: SerializeField] public Color OutlineColorOnGrab { get; private set; } = Color.white;
        [field: SerializeField] public float OutlineWidthOnGrab { get; private set; } = 5f;
    }
}