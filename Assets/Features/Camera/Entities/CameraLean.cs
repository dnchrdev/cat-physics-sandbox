using UnityEngine;

namespace Features.Camera.Entities
{
    public class CameraLean
    {
        private const float AccelerationEpsilonSqr = 0.0001f;
        private const float AccelerationStopMagnitude = 0.001f;
        private const float DirectionAlignmentThreshold = 0.5f;

        private readonly LeanConfig _config;

        private Vector3 _dampedAcceleration;
        private Vector3 _dampedAccelerationVelocity;
        private float _smoothStrength;
        private bool _isInitialized;

        public CameraLean(LeanConfig config)
        {
            _config = config;
        }

        public Quaternion Advance(LeanInput input)
        {
            if (_isInitialized == false)
            {
                _smoothStrength = _config.MoveStrength;
                _isInitialized = true;
            }

            var planarAcceleration = Vector3.ProjectOnPlane(input.Acceleration, Vector3.up);

            float damping;
            if (planarAcceleration.sqrMagnitude < AccelerationEpsilonSqr)
            {
                damping = _config.DecayDamping;
            }
            else if (_dampedAcceleration.sqrMagnitude < AccelerationEpsilonSqr)
            {
                damping = _config.AttackDamping;
            }
            else
            {
                var dot = Vector3.Dot(
                    _dampedAcceleration.normalized,
                    planarAcceleration.normalized);
                damping = dot < DirectionAlignmentThreshold ? _config.AttackDamping : _config.DecayDamping;
            }

            _dampedAcceleration = Vector3.SmoothDamp(
                _dampedAcceleration,
                planarAcceleration,
                ref _dampedAccelerationVelocity,
                damping,
                float.PositiveInfinity,
                input.DeltaTime);

            if (_dampedAcceleration.magnitude < AccelerationStopMagnitude &&
                planarAcceleration.magnitude < AccelerationStopMagnitude)
            {
                _dampedAcceleration = Vector3.zero;
                _dampedAccelerationVelocity = Vector3.zero;
            }

            var targetStrength = input.IsSliding ? _config.SlideStrength : _config.MoveStrength;
            _smoothStrength = Mathf.Lerp(
                _smoothStrength,
                targetStrength,
                1f - Mathf.Exp(-_config.StrengthResponse * input.DeltaTime));

            var lateralAccel = Vector3.Dot(_dampedAcceleration, input.RootRight);
            var forwardAccel = Vector3.Dot(_dampedAcceleration, input.RootForward);
            var strength = _smoothStrength;

            var lateralLean = Quaternion.AngleAxis(-lateralAccel * strength, Vector3.forward);
            var forwardLean = Quaternion.AngleAxis(-forwardAccel * strength, Vector3.right);

            return lateralLean * forwardLean;
        }
    }
    
    public readonly struct LeanInput
    {
        public readonly Vector3 Acceleration;
        public readonly bool IsSliding;
        public readonly Vector3 RootForward;
        public readonly Vector3 RootRight;
        public readonly float DeltaTime;

        public LeanInput(Vector3 acceleration, bool isSliding, Vector3 rootForward, Vector3 rootRight, float deltaTime)
        {
            Acceleration = acceleration;
            IsSliding = isSliding;
            RootForward = rootForward;
            RootRight = rootRight;
            DeltaTime = deltaTime;
        }
    }

    public readonly struct LeanConfig
    {
        public readonly float AttackDamping;
        public readonly float DecayDamping;
        public readonly float MoveStrength;
        public readonly float SlideStrength;
        public readonly float StrengthResponse;

        public LeanConfig(float attackDamping, float decayDamping, float moveStrength, float slideStrength,
            float strengthResponse)
        {
            AttackDamping = attackDamping;
            DecayDamping = decayDamping;
            MoveStrength = moveStrength;
            SlideStrength = slideStrength;
            StrengthResponse = strengthResponse;
        }
    }
    
}
