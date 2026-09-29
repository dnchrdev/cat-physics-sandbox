using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;
using Features.Shared.Entities;

namespace Features.Enemy.UseCases
{
    public sealed class EnemyLifecycleUseCase
    {
        private readonly Entities.Enemy _enemy;
        private readonly KnockoutSettings _settings;
        private readonly EnemyAttackUseCase _attack;
        private readonly WalkAnimationUseCase _walkAnimation;
        private readonly IEnemyAnimationOutput _animation;
        private readonly IRagdoll _ragdoll;
        private readonly IEnemyMover _mover;
        private readonly IEnemyHead _head;
        private readonly IPatrolPoints _patrolPoints;

        public EnemyLifecycleUseCase(
            Entities.Enemy enemy,
            KnockoutSettings settings,
            EnemyAttackUseCase attack,
            WalkAnimationUseCase walkAnimation,
            IEnemyAnimationOutput animation,
            IRagdoll ragdoll,
            IEnemyMover mover,
            IEnemyHead head,
            IPatrolPoints patrolPoints)
        {
            _enemy = enemy;
            _settings = settings;
            _attack = attack;
            _walkAnimation = walkAnimation;
            _animation = animation;
            _ragdoll = ragdoll;
            _mover = mover;
            _head = head;
            _patrolPoints = patrolPoints;
        }

        public void Tick(float dt)
        {
            _enemy.Tick(dt);

            if (_enemy.CanRecover(_settings.RestDuration, _ragdoll.IsAnyBoneGrabbed))
                Recover();
        }

        public void HandleHit(AttackInfo attackInfo)
        {
            if (attackInfo.HitVelocity < _settings.MinHitVelocity)
                return;

            _attack.Cancel();
            _enemy.Knockout();
            _animation.SetAnimated(false);
            _ragdoll.Enable();
        }

        public void HandleRagdollDisturbed() => _enemy.Disturb();

        public void HandlePlayerRespawned()
        {
            _attack.Cancel();
            _enemy.CalmDown();
        }

        private void Recover()
        {
            _attack.Cancel();

            var nearestIndex = _patrolPoints.GetNearestPatrolIndex(_head.HeadPosition);
            _mover.Teleport(_patrolPoints.GetPatrolPointPosition(nearestIndex));
            _walkAnimation.Reset();

            _enemy.Recover(_settings.AngerDuration);
            _ragdoll.Disable();
            _animation.SetAnimated(true);
        }
    }
}
