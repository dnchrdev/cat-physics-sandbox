using Features.Camera.Entities;
using Features.Camera.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Camera.UseCases
{
    public sealed class FollowEnemyHeadUseCase
    {
        private readonly CameraSettings _settings;
        private readonly IEnemyHeadPositionProvider _headPositions;
        private readonly ICameraMover _mover;

        private IEntity _followedEnemy;

        public FollowEnemyHeadUseCase(CameraSettings settings, IEnemyHeadPositionProvider headPositions, ICameraMover mover)
        {
            _settings = settings;
            _headPositions = headPositions;
            _mover = mover;
        }

        public void UpdateFollowedEnemy(AttackInfo info) => _followedEnemy = info.Entity;

        public void Tick(float dt)
        {
            Vector3? followedPosition = _followedEnemy != null
                ? _headPositions.GetHeadPosition(_followedEnemy)
                : null;

            var rotation = DeadCameraRotationCalculator.Calculate(
                _mover.RotationRootRotation,
                _mover.RotationRootForward,
                followedPosition,
                _mover.PositionRootPosition,
                _settings.DeadPlayerTurnSpeedResponse,
                dt);

            _mover.SetRotation(rotation);
        }
    }
}
