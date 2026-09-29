using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class AttackAngleCalculator
    {
        public static bool IsAngleValid(
            Vector3 enemyPosition,
            Vector3 enemyForward,
            Vector3 targetPosition,
            float maxAngle)
        {
            var directionToTarget = Vector3.ProjectOnPlane(targetPosition - enemyPosition, Vector3.up).normalized;
            var angle = Vector3.Angle(directionToTarget, enemyForward);
            return angle <= maxAngle;
        }
    }
}
