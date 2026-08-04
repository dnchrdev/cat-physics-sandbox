using System;
using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [Serializable]
    public struct ColliderDrag
    {
        [field: SerializeField] public float Drag { get; private set; }
        [field: SerializeField] public float AngularDrag { get; private set; }
    }

    [CreateAssetMenu(fileName = "InteractablePhysicsConfig", menuName = "SO/InteractablePhysicsConfig")]
    public class PhysicsConfig : ScriptableObject
    {
        [field: Header("Default")]
        [field: SerializeField] public PhysicsMaterial DefaultMaterial { get; private set; }
        [field: SerializeField] public ColliderDrag DefaultDrag { get; private set; }

        [field: Header("Interact")]
        [field: SerializeField] public PhysicsMaterial InteractMaterial { get; private set; }
        [field: SerializeField] public ColliderDrag InteractDrag { get; private set; }
    }
}