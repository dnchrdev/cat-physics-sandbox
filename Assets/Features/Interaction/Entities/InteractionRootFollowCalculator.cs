using UnityEngine;

namespace Features.Interaction.Entities
{
    public static class InteractionRootFollowCalculator
    {
        private const float MinDistance = 0.0001f;

        public static Vector3 AngularVelocity(Quaternion current, Quaternion target, float smoothing, float deltaTime)
        {
            var desired = Quaternion.Slerp(current, target, 1f - Mathf.Exp(-smoothing * deltaTime));
            var delta = desired * Quaternion.Inverse(current);
            return new Vector3(delta.x, delta.y, delta.z) * 2f / deltaTime;
        }

        public static Vector3 Velocity(Vector3 current, Vector3 target, float radius, float maxSpeed, float deltaTime)
        {
            var delta = target - current;
            var distance = delta.magnitude;
            if (distance <= MinDistance) return Vector3.zero;

            var speed = maxSpeed * Mathf.Min(distance / radius, 1f);
            var maxSpeedThisFrame = distance / deltaTime;
            speed = Mathf.Min(maxSpeed, Mathf.Min(speed, maxSpeedThisFrame));
            return delta.normalized * speed;
        }
    }
}
