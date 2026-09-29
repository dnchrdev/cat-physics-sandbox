using Features.Enemy.Entities.Calculators;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.UseCases
{
    public sealed class WalkAnimationUseCase
    {
        private readonly WalkAnimationSettings _settings;
        private readonly IEnemyMover _mover;
        private readonly IEnemyAnimationOutput _output;

        private bool _isPursuing;
        private Vector3 _previousPosition;
        private float _previousSpeed;

        public WalkAnimationUseCase(WalkAnimationSettings settings, IEnemyMover mover, IEnemyAnimationOutput output)
        {
            _settings = settings;
            _mover = mover;
            _output = output;
        }

        public void Reset()
        {
            _previousPosition = _mover.Position;
            _previousSpeed = 0f;
        }

        public void SetPursuit(bool isPursuing)
        {
            _isPursuing = isPursuing;
            _output.SetPursuit(isPursuing);
        }

        public void Update(float dt)
        {
            var maxSpeed = _isPursuing ? _settings.PursuitSpeed : _settings.PatrolSpeed;
            var maxAnimationSpeed = _isPursuing ? _settings.PursuitAnimationSpeed : _settings.PatrolAnimationSpeed;
            var position = _mover.Position;

            var speed = WalkAnimationSpeedCalculator.Calculate(
                _previousPosition, position, dt, maxSpeed, maxAnimationSpeed, _previousSpeed, _settings.Response);

            _previousPosition = position;
            _previousSpeed = speed;
            _output.SetWalkSpeed(speed);
        }
    }
}
