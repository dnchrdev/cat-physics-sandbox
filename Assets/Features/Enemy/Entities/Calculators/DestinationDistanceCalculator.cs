using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class DestinationDistanceCalculator
    {
        public static float CalculateHorizontalDistance(Vector3 fromPosition, Vector3 toPosition)
        {
            return Vector3.ProjectOnPlane(toPosition - fromPosition, Vector3.up).magnitude;
        }

        public static bool IsDestinationReached(Vector3 fromPosition, Vector3 toPosition, float stoppingDistance)
        {
            return CalculateHorizontalDistance(fromPosition, toPosition) <= stoppingDistance;
        }
    }
}
