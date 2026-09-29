using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class HeadLookCalculator
    {
        public static bool IsWithinLookAngle(
            Vector3 headPosition,
            Vector3 enemyForward,
            Vector3 targetPosition,
            float yawMaxAngle,
            float pitchMaxAngle)
        {
            var directionToTarget = targetPosition - headPosition;
            var directionFlat = Vector3.ProjectOnPlane(directionToTarget, Vector3.up).normalized;
            var forwardFlat = Vector3.ProjectOnPlane(enemyForward, Vector3.up);
            var yawAngle = Vector3.Angle(forwardFlat, directionFlat);
            var pitchAngle = Mathf.Abs(Vector3.Angle(directionFlat, directionToTarget));
            return yawAngle <= yawMaxAngle && pitchAngle <= pitchMaxAngle;
        }

        public static float CalculateSmoothedWeight(
            float currentWeight,
            bool withinAngle,
            bool isHeadLookingEnabled,
            bool isTargetVisible,
            float response,
            float dt)
        {
            var targetWeight = (withinAngle && isHeadLookingEnabled && isTargetVisible) ? 1f : 0f;
            return Mathf.Lerp(currentWeight, targetWeight, 1f - Mathf.Exp(-response * dt));
        }
    }
}
