using Feature.PlayerFeature;
using UnityEngine;
using Zenject;

namespace Feature.CameraFeature
{
    public class CameraLean
    {
        [Inject] private readonly IReadOnlyMovementState _playerMovementState;
        
        private readonly Transform _root;
        private readonly CameraConfig _config;

        private Vector3 _dampedAcceleration;
        private Vector3 _dampedAccelerationVel;
        private float _smoothStrength;

        public CameraLean(CameraRig cameraRig, CameraConfig config, IReadOnlyMovementState readOnlyMovementState)
        {
            _root = cameraRig.LeanRoot;
            _config = config;
            _smoothStrength = _config.MoveStrength;
        }

        public void Tick(float dt)
        {
            _root.localRotation = Quaternion.identity;

            var planarAcceleration = Vector3.ProjectOnPlane(_playerMovementState.GetAcceleration(), Vector3.up);

            float damping;
            if (planarAcceleration.sqrMagnitude < 0.0001f)
            {
                damping = _config.DecayDamping;
            }
            else if (_dampedAcceleration.sqrMagnitude < 0.0001f)
            {
                damping = _config.AttackDamping;
            }
            else
            {
                var dot = Vector3.Dot(
                    _dampedAcceleration.normalized,
                    planarAcceleration.normalized
                );
                damping = dot < 0.5f ? _config.AttackDamping : _config.DecayDamping;
            }

            _dampedAcceleration = Vector3.SmoothDamp(
                _dampedAcceleration,
                planarAcceleration,
                ref _dampedAccelerationVel,
                damping,
                float.PositiveInfinity,
                dt
            );

            if (_dampedAcceleration.magnitude < 0.001f &&
                planarAcceleration.magnitude < 0.001f)
            {
                _dampedAcceleration = Vector3.zero;
                _dampedAccelerationVel = Vector3.zero;
            }

            var targetStrength = _playerMovementState.IsSliding() ? _config.SlideStrength : _config.MoveStrength;
            _smoothStrength = Mathf.Lerp(
                _smoothStrength,
                targetStrength,
                1f - Mathf.Exp(-_config.StrengthResponse * dt)
            );

            var forward = Vector3.ProjectOnPlane(_root.rotation * Vector3.forward, Vector3.up).normalized;
            var right = Vector3.ProjectOnPlane(_root.rotation * Vector3.right, Vector3.up).normalized;
            var lateralAccel = Vector3.Dot(_dampedAcceleration, right);
            var forwardAccel = Vector3.Dot(_dampedAcceleration, forward);
            var strength = _smoothStrength;
            var lateralLean = Quaternion.AngleAxis(-lateralAccel * strength, Vector3.forward);

            var forwardLean = Quaternion.AngleAxis(-forwardAccel * strength, Vector3.right);
            _root.rotation = _root.rotation * lateralLean * forwardLean;
        }
    }
}