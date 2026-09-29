using UnityEngine;

namespace Features.Camera.Entities
{
    public class CameraHeadbob
    {
        private const float MinOffsetSqrMagnitude = 0.000001f;

        private readonly HeadbobConfig _config;

        private float _bobTime;
        private float _currentAmplitude;
        private float _currentFrequency;
        private Vector3 _currentOffset;
        private Vector3 _currentOffsetVelocity;

        public CameraHeadbob(HeadbobConfig config)
        {
            _config = config;
        }

        public Vector3 Advance(HeadbobInput input)
        {
            var speedRange = _config.SpeedMax - _config.SpeedMin;
            var speedNorm = speedRange > 0f
                ? Mathf.Clamp01((input.Speed - _config.SpeedMin) / speedRange)
                : 0f;

            float targetAmplitude;
            float targetFrequency;

            if (input.IsGrounded == false)
            {
                targetAmplitude = 0f;
                targetFrequency = 0f;
            }
            else if (input.IsSliding)
            {
                targetAmplitude = _config.SlideAmplitude;
                targetFrequency = _config.SlideFrequency;
            }
            else
            {
                targetAmplitude = Mathf.Lerp(_config.AmplitudeMin, _config.AmplitudeMax, speedNorm);
                targetFrequency = Mathf.Lerp(_config.FrequencyMin, _config.FrequencyMax, speedNorm);
            }

            _currentAmplitude = Mathf.Lerp(
                _currentAmplitude,
                targetAmplitude,
                1f - Mathf.Exp(-_config.AmplitudeResponse * input.DeltaTime));

            _currentFrequency = Mathf.Lerp(
                _currentFrequency,
                targetFrequency,
                1f - Mathf.Exp(-_config.FrequencyResponse * input.DeltaTime));

            if (input.IsGrounded)
                _bobTime += input.DeltaTime * _currentFrequency;

            var vertical = Mathf.Sin(_bobTime * Mathf.PI * 2f);

            var targetOffset = new Vector3(0f, vertical * _currentAmplitude, 0f);

            bool shouldDecay = input.IsGrounded == false || targetAmplitude < 0.0001f;

            if (shouldDecay)
            {
                _currentOffset = Vector3.SmoothDamp(
                    _currentOffset,
                    Vector3.zero,
                    ref _currentOffsetVelocity,
                    _config.DecayDamping,
                    float.PositiveInfinity,
                    input.DeltaTime);

                if (_currentOffset.sqrMagnitude < MinOffsetSqrMagnitude)
                {
                    _currentOffset = Vector3.zero;
                    _currentOffsetVelocity = Vector3.zero;
                }
            }
            else
            {
                _currentOffset = targetOffset;
            }

            return _currentOffset;
        }
    }

    public readonly struct HeadbobInput
    {
        public readonly float Speed;
        public readonly bool IsGrounded;
        public readonly bool IsSliding;
        public readonly float DeltaTime;

        public HeadbobInput(float speed, bool isGrounded, bool isSliding, float deltaTime)
        {
            Speed = speed;
            IsGrounded = isGrounded;
            IsSliding = isSliding;
            DeltaTime = deltaTime;
        }
    }

    public readonly struct HeadbobConfig
    {
        public readonly float SpeedMin;
        public readonly float SpeedMax;
        public readonly float AmplitudeMin;
        public readonly float AmplitudeMax;
        public readonly float FrequencyMin;
        public readonly float FrequencyMax;
        public readonly float AmplitudeResponse;
        public readonly float FrequencyResponse;
        public readonly float DecayDamping;
        public readonly float SlideAmplitude;
        public readonly float SlideFrequency;

        public HeadbobConfig(
            float speedMin, float speedMax,
            float amplitudeMin, float amplitudeMax,
            float frequencyMin, float frequencyMax,
            float amplitudeResponse, float frequencyResponse,
            float decayDamping,
            float slideAmplitude, float slideFrequency)
        {
            SpeedMin = speedMin;
            SpeedMax = speedMax;
            AmplitudeMin = amplitudeMin;
            AmplitudeMax = amplitudeMax;
            FrequencyMin = frequencyMin;
            FrequencyMax = frequencyMax;
            AmplitudeResponse = amplitudeResponse;
            FrequencyResponse = frequencyResponse;
            DecayDamping = decayDamping;
            SlideAmplitude = slideAmplitude;
            SlideFrequency = slideFrequency;
        }
    }
}
