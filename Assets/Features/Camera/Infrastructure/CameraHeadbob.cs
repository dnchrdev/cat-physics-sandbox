using Feature.PlayerFeature;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class CameraHeadbob
    {
        [Inject] private readonly CameraConfig _config;
        [Inject] private readonly IReadOnlyMovementState _playerMovementState;
        
        private readonly Transform _root;

        private float _bobTime;
        private float _currentAmplitude;
        private float _currentFrequency;
        private Vector3 _currentOffset;
        private Vector3 _currentOffsetVel;

        public CameraHeadbob(CameraRig cameraRig)
        {
            _root = cameraRig.BobRoot;
        }

        public void Tick(float dt)
        {
            _root.localPosition = Vector3.zero;

            var speed = _playerMovementState.GetVelocity().magnitude;

            var speedRange = _config.BobSpeedMax - _config.BobSpeedMin;
            var speedNorm = speedRange > 0f
                ? Mathf.Clamp01((speed - _config.BobSpeedMin) / speedRange)
                : 0f;

            float targetAmplitude;
            float targetFrequency;

            if (_playerMovementState.IsGrounded() == false)
            {
                targetAmplitude = 0f;
                targetFrequency = 0f;
            }
            else 
            if (_playerMovementState.IsSliding())
            {
                targetAmplitude = _config.BobSlideAmplitude;
                targetFrequency = _config.BobSlideFrequency;
            }
            else
            {
                targetAmplitude = Mathf.Lerp(_config.BobAmplitudeMin, _config.BobAmplitudeMax, speedNorm);
                targetFrequency = Mathf.Lerp(_config.BobFrequencyMin, _config.BobFrequencyMax, speedNorm);
            }

            _currentAmplitude = Mathf.Lerp(
                _currentAmplitude,
                targetAmplitude,
                1f - Mathf.Exp(-_config.BobAmplitudeResponse * dt)
            );

            _currentFrequency = Mathf.Lerp(
                _currentFrequency,
                targetFrequency,
                1f - Mathf.Exp(-_config.BobFrequencyResponse * dt)
            );

            if (_playerMovementState.IsGrounded())
                _bobTime += dt * _currentFrequency;

            var vertical = Mathf.Sin(_bobTime * Mathf.PI * 2f);
            var horizontal = Mathf.Sin(_bobTime * Mathf.PI);

            var targetOffset = new Vector3(
                horizontal * _currentAmplitude * _config.BobHorizontalMultiplier,
                vertical * _currentAmplitude,
                0f
            );

            bool shouldDecay = !_playerMovementState.IsGrounded() ||
                               targetAmplitude < 0.0001f;

            if (shouldDecay)
            {
                _currentOffset = Vector3.SmoothDamp(
                    _currentOffset,
                    Vector3.zero,
                    ref _currentOffsetVel,
                    _config.BobDecayDamping,
                    float.PositiveInfinity,
                    dt
                );

                if (_currentOffset.sqrMagnitude < 0.000001f)
                {
                    _currentOffset = Vector3.zero;
                    _currentOffsetVel = Vector3.zero;
                }
            }
            else
            {
                _currentOffset = targetOffset;
            }

            _root.localPosition = _currentOffset;
        }
        
    }
}