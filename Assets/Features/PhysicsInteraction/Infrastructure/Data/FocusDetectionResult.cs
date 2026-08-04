using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public readonly struct FocusDetectionResult
    {
        public readonly Collider Collider;
        public readonly Vector3 ContactPoint;
        public readonly bool HasHit;

        public FocusDetectionResult(Collider collider, Vector3 contactPoint)
        {
            Collider = collider;
            ContactPoint = contactPoint;
            HasHit = true;
        }

        public static FocusDetectionResult None => default;
    }
}