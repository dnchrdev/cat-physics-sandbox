using UnityEngine;

namespace Features.Interaction.Entities
{
    public readonly struct InteractableSettings
    {
        public readonly bool IsRagdoll;
        public readonly bool IsKinematicOnStart;
        public readonly GrabType GrabType;
        public readonly bool CanBeHit;
        public readonly bool DamagesOnCollision;
        public readonly bool DamagesOnTrigger;
        public readonly Vector3 HandAnchorDirection;
        public readonly bool UseCustomHandDistance;
        public readonly float CustomHandDistance;
        public readonly bool ShowsGrabPoint;

        public InteractableSettings(
            bool isRagdoll,
            bool isKinematicOnStart,
            GrabType grabType,
            bool canBeHit,
            bool damagesOnCollision,
            bool damagesOnTrigger,
            Vector3 handAnchorDirection,
            bool useCustomHandDistance,
            float customHandDistance,
            bool showsGrabPoint)
        {
            IsRagdoll = isRagdoll;
            IsKinematicOnStart = isKinematicOnStart;
            GrabType = grabType;
            CanBeHit = canBeHit;
            DamagesOnCollision = damagesOnCollision;
            DamagesOnTrigger = damagesOnTrigger;
            HandAnchorDirection = handAnchorDirection;
            UseCustomHandDistance = useCustomHandDistance;
            CustomHandDistance = customHandDistance;
            ShowsGrabPoint = showsGrabPoint;
        }

        public bool CanGrab => GrabType != GrabType.None;

        public InteractionType InteractionType => (CanBeHit, CanGrab) switch
        {
            (true, true) => InteractionType.Full,
            (true, false) => InteractionType.Hit,
            (false, true) => InteractionType.Grab,
            _ => InteractionType.None
        };
    }
}
