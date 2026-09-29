using UnityEngine;

namespace Features.Camera.Entities
{
    public static class DeadCameraRotationCalculator
    {
        public static Quaternion Calculate(
            Quaternion currentRotation,
            Vector3 currentForward,
            Vector3? followedTargetPosition,
            Vector3 cameraPosition,
            float turnSpeedResponse,
            float deltaTime)
        {
            var directionFollow = currentForward;

            if (followedTargetPosition.HasValue)
                directionFollow = followedTargetPosition.Value - cameraPosition;

            var euler = Quaternion.LookRotation(directionFollow, Vector3.up).eulerAngles;
            var targetRotation = Quaternion.Euler(euler.x, euler.y, 0f);

            return Quaternion.Slerp(
                currentRotation,
                targetRotation,
                1f - Mathf.Exp(-turnSpeedResponse * deltaTime));
        }
    }
}
