using System;
using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
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