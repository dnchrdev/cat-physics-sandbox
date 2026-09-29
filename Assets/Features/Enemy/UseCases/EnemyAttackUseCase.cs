using Features.Enemy.Entities;
using Features.Enemy.Entities.Calculators;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;

namespace Features.Enemy.UseCases
{
    public sealed class EnemyAttackUseCase
    {
        private readonly EnemyAttack _attack;
        private readonly Entities.Enemy _enemy;
        private readonly AttackSettings _settings;
        private readonly IEnemyMover _mover;
        private readonly IEnemyHead _head;
        private readonly IEnemyAnimationOutput _animation;
        private readonly IReadOnlyPlayer _player;
        private readonly IPlayerTarget _playerTarget;

        public EnemyAttackUseCase(
            EnemyAttack attack,
            Entities.Enemy enemy,
            AttackSettings settings,
            IEnemyMover mover,
            IEnemyHead head,
            IEnemyAnimationOutput animation,
            IReadOnlyPlayer player,
            IPlayerTarget playerTarget)
        {
            _attack = attack;
            _enemy = enemy;
            _settings = settings;
            _mover = mover;
            _head = head;
            _animation = animation;
            _player = player;
            _playerTarget = playerTarget;
        }

        public bool IsPlaying => _attack.IsPlaying;
        public bool IsReady => _attack.IsReady;

        public void Tick(float dt) => _attack.Tick(dt);

        public bool IsTargetInReach() =>
            CanHitTargetCalculator.CanHit(
                _head.HeadPosition,
                _player.Position,
                _settings.SphereDistance,
                _settings.HorizontalDistance);

        public bool IsFacingTarget(float maxYawAngle) =>
            AttackAngleCalculator.IsAngleValid(_mover.Position, _mover.Forward, _player.Position, maxYawAngle);

        public void AimAndStrike(float dt)
        {
            if (IsFacingTarget(_settings.StartMaxYawAngle) && _attack.TryBegin(_settings.Cooldown))
                _animation.PlayAttack();

            TurnToTarget(dt);
        }

        public void Cancel()
        {
            _attack.Stop();
            _animation.CancelAttack();
        }

        public void HandleSwingStarted() => _attack.MarkPlaying();
        public void HandleSwingFinished() => _attack.Stop();

        public void HandleDamageFrame()
        {
            if (!_enemy.IsAlive)
                return;

            if (IsTargetInReach() && IsFacingTarget(_settings.DamageMaxYawAngle))
                _playerTarget.ReceiveHit(new AttackInfo(_enemy, false));
        }

        private void TurnToTarget(float dt)
        {
            var rotation = AttackRotationCalculator.CalculateRotationTowardsTarget(
                _mover.Position,
                _mover.Rotation,
                _player.Position,
                _settings.RotationSpeed,
                dt);
            _mover.SetRotation(rotation);
        }
    }
}
