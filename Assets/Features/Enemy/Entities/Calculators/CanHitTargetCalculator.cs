using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class CanHitTargetCalculator
    {
        public static bool CanHit(
            Vector3 headPosition,
            Vector3 targetPosition,
            float sphereDistance,
            float horizontalDistance)
        {
            if (Vector3.Dot(targetPosition - headPosition, Vector3.up) >= 0f)
            {
                return Vector3.Distance(headPosition, targetPosition) <= sphereDistance;
            }
            var horizontalDelta = Vector3.ProjectOnPlane(headPosition - targetPosition, Vector3.up);
            return horizontalDelta.magnitude <= horizontalDistance;
        }
    }
}
