using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class AttackRotationCalculator
    {
        public static Quaternion CalculateRotationTowardsTarget(
            Vector3 enemyPosition,
            Quaternion currentRotation,
            Vector3 targetPosition,
            float rotationSpeed,
            float dt)
        {
            var targetDirection = Vector3.ProjectOnPlane(targetPosition - enemyPosition, Vector3.up).normalized;
            var targetRotation = Quaternion.LookRotation(targetDirection);
            return Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * dt);
        }
    }
}
