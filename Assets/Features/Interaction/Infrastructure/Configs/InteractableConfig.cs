using System;
using Features.Interaction.Entities;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Configs
{
    [CreateAssetMenu(fileName = "InteractableConfig", menuName = "Configs/InteractableConfig")]
    public sealed class InteractableConfig : ScriptableObject
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

        public InteractableSettings ToSettings()
        {
            if (InteractableVisualConfig == null || InteractablePhysicsConfig == null)
                throw new NullReferenceException($"{name}: visual and physics configs must not be null");

            return new InteractableSettings(
                isRagdoll: IsRagdoll,
                isKinematicOnStart: IsKinematicOnStart,
                grabType: GrabType,
                canBeHit: HitType == HitType.Impulse,
                damagesOnCollision: CollisionHitType == CollisionHitType.CollisionHit,
                damagesOnTrigger: CollisionTriggerType == CollisionTriggerType.CollisionTrigger,
                handAnchorDirection: AnchorDirection,
                useCustomHandDistance: UseCustomHandDistance,
                customHandDistance: CustomHandDistance,
                showsGrabPoint: InteractableVisualConfig.UseGrabPositionIndication);
        }
    }
}
