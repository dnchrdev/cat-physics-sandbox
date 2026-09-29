using Features.Enemy.Entities;
using Features.Enemy.Entities.Calculators;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Enemy.UseCases
{
    public sealed class EnemyHeadLookUseCase
    {
        private readonly HeadLook _headLook;
        private readonly HeadLookSettings _settings;
        private readonly IEnemyHead _head;
        private readonly IEnemyMover _mover;
        private readonly IEnemyVision _vision;
        private readonly IReadOnlyPlayer _player;
        private readonly IEnemyAnimationOutput _animation;

        public EnemyHeadLookUseCase(
            HeadLook headLook,
            HeadLookSettings settings,
            IEnemyHead head,
            IEnemyMover mover,
            IEnemyVision vision,
            IReadOnlyPlayer player,
            IEnemyAnimationOutput animation)
        {
            _headLook = headLook;
            _settings = settings;
            _head = head;
            _mover = mover;
            _vision = vision;
            _player = player;
            _animation = animation;
        }

        public void Tick(float dt)
        {
            var target = _player.Position;
            var isVisible = _vision.CanSee(target);
            var isWithinAngle = HeadLookCalculator.IsWithinLookAngle(
                _head.HeadPosition,
                _mover.Forward,
                target,
                _settings.YawMaxAngle,
                _settings.PitchMaxAngle);

            _headLook.UpdateWeight(isWithinAngle, isVisible, _settings.Response, dt);
            _animation.SetHeadLook(target, _headLook.Weight);
        }
    }
}
