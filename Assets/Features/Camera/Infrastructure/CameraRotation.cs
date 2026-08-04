using Feature.Storage;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class CameraRotation
    {
        [Inject] private readonly CameraConfig _config;
        [Inject] private readonly IReadOnlyControlSettings _controlSettings;
        
        private readonly Transform _root;

        private float _yaw;
        private float _pitch;

        public CameraRotation(CameraRig cameraRig)
        {
            _root = cameraRig.RotationRoot;
        }

        public void Tick(Vector2 deltaLook, float dt)
        {
            deltaLook = deltaLook * _controlSettings.LookSensitivity / 100f;

            _yaw += deltaLook.x;
            _pitch = Mathf.Clamp(_pitch - deltaLook.y, _config.MinPitchLimit, _config.MaxPitchLimit);

            Quaternion targetRotation = Quaternion.Euler(_pitch, _yaw, 0f);

            _root.rotation = Quaternion.Slerp(_root.rotation, targetRotation, 1f - Mathf.Exp(-_config.TurnSpeedSmoothing * dt));
        }
    }
}