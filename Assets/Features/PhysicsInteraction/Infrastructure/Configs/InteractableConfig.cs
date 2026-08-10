using System;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    [CreateAssetMenu(fileName = "InteractableConfig", menuName = "Configs/InteractableConfig")]
    public class InteractableConfig : ScriptableObject
    {
        [field: SerializeField] public bool IsRagdoll { get; private set; }
        [field: SerializeField] public bool IsKinematicOnStart { get; private set; }
        [field: SerializeField] public bool UseCustomHandDistance { get; private set; }
        [field: SerializeField] public float CustomHandDistance { get; private set; } = 0.25f;
        [field: SerializeField] public Vector3 AnchorDirection { get; private set; }
        [field: SerializeField] public InteractableVisualConfig InteractableVisualConfig { get; private set; }
        [field: SerializeField] public InteractablePhysicsConfig InteractablePhysicsConfig { get; private set; }
        [field: SerializeField] public HitType HitType { get; private set; }
        [field: SerializeField] public GrabType GrabType { get; private set; }
        [field: SerializeField] public CollisionHitType CollisionHitType { get; private set; }
        [field: SerializeField] public CollisionTriggerType CollisionTriggerType { get; private set; }

        private void OnValidate()
        {
            AnchorDirection = AnchorDirection.normalized;
            if (AnchorDirection.sqrMagnitude < 0.001f || AnchorDirection.z < 0f) throw new Exception("Invalid anchor direction");
            if(InteractableVisualConfig == null ||  InteractablePhysicsConfig == null) throw new Exception("Configs must not be null");
        }
    }
}