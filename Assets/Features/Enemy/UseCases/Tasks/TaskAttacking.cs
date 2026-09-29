using Features.Enemy.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Enemy.UseCases.Tasks
{
    public sealed class TaskAttacking : IEnemyTask
    {
        private readonly Entities.Enemy _enemy;
        private readonly EnemyAttackUseCase _attack;
        private readonly WalkAnimationUseCase _walkAnimation;
        private readonly IEnemyMover _mover;
        private readonly IEnemyVision _vision;
        private readonly IReadOnlyPlayer _player;

        public TaskAttacking(
            Entities.Enemy enemy,
            EnemyAttackUseCase attack,
            WalkAnimationUseCase walkAnimation,
            IEnemyMover mover,
            IEnemyVision vision,
            IReadOnlyPlayer player)
        {
            _enemy = enemy;
            _attack = attack;
            _walkAnimation = walkAnimation;
            _mover = mover;
            _vision = vision;
            _player = player;
        }

        public TaskResult Evaluate(float dt)
        {
            if (!_enemy.IsAngry && !_attack.IsPlaying)
                return TaskResult.Failure;

            if (_attack.IsPlaying)
            {
                _mover.Stop();
                _walkAnimation.Update(dt);
                _walkAnimation.SetPursuit(false);
                return TaskResult.Running;
            }

            if (_enemy.TargetPlayer.IsAlive && CanStrike())
            {
                _attack.AimAndStrike(dt);
                return TaskResult.Running;
            }

            return TaskResult.Failure;
        }

        private bool CanStrike() =>
            _attack.IsReady &&
            _attack.IsTargetInReach() &&
            _vision.CanSee(_player.Position);
    }
}
