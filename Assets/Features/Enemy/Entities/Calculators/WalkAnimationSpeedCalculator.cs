using Features.Shared.Utilities;
using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class WalkAnimationSpeedCalculator
    {
        public static float Calculate(
            Vector3 previousPosition,
            Vector3 currentPosition,
            float dt,
            float maxSpeed,
            float maxAnimationSpeed,
            float previousWalkSpeed,
            float response)
        {
            var velocity = Vector3.ProjectOnPlane(previousPosition - currentPosition, Vector3.up).magnitude / dt;
            var walkSpeed = MathExtensions.Map(velocity, 0f, maxSpeed, 0f, maxAnimationSpeed);
            var validPreviousWalkSpeed = float.IsNaN(previousWalkSpeed) ? 0f : previousWalkSpeed;
            walkSpeed = Mathf.Lerp(validPreviousWalkSpeed, walkSpeed, 1f - Mathf.Exp(-response * dt));
            return walkSpeed;
        }
    }
}
