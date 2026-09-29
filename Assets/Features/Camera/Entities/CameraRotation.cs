using UnityEngine;

namespace Features.Camera.Entities
{
    public class CameraRotation
    {
        private const float SENSITIVITY_DEVISOR = 100f;

        private readonly RotationConfig _config;

        private float _yaw;
        private float _pitch;

        public CameraRotation(RotationConfig config)
        {
            _config = config;
        }

        public Quaternion Advance(Vector2 lookDelta, float lookSensitivity, Quaternion currentRotation, float deltaTime)
        {
            var scaledDelta = lookDelta * lookSensitivity / SENSITIVITY_DEVISOR;

            _yaw += scaledDelta.x;
            _pitch = Mathf.Clamp(_pitch - scaledDelta.y, _config.MinPitchLimit, _config.MaxPitchLimit);

            var targetRotation = Quaternion.Euler(_pitch, _yaw, 0f);

            return Quaternion.Slerp(
                currentRotation,
                targetRotation,
                1f - Mathf.Exp(-_config.TurnSpeedSmoothing * deltaTime));
        }
    }
    
    public readonly struct RotationConfig
    {
        public readonly float MinPitchLimit;
        public readonly float MaxPitchLimit;
        public readonly float TurnSpeedSmoothing;

        public RotationConfig(float minPitchLimit, float maxPitchLimit, float turnSpeedSmoothing)
        {
            MinPitchLimit = minPitchLimit;
            MaxPitchLimit = maxPitchLimit;
            TurnSpeedSmoothing = turnSpeedSmoothing;
        }
    }
    
}
