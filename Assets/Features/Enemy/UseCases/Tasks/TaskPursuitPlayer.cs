using Features.Enemy.Entities;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Enemy.UseCases.Tasks
{
    public sealed class TaskPursuitPlayer : IEnemyTask
    {
        private readonly Entities.Enemy _enemy;
        private readonly HeadLook _headLook;
        private readonly PursuitSettings _settings;
        private readonly WalkAnimationUseCase _walkAnimation;
        private readonly IEnemyMover _mover;
        private readonly IReadOnlyPlayer _player;

        public TaskPursuitPlayer(
            Entities.Enemy enemy,
            HeadLook headLook,
            PursuitSettings settings,
            WalkAnimationUseCase walkAnimation,
            IEnemyMover mover,
            IReadOnlyPlayer player)
        {
            _enemy = enemy;
            _headLook = headLook;
            _settings = settings;
            _walkAnimation = walkAnimation;
            _mover = mover;
            _player = player;
        }

        public TaskResult Evaluate(float dt)
        {
            if (_enemy.IsAngry && _player.IsAlive)
            {
                _mover.MoveTo(_player.Position, _settings.Speed);
                _walkAnimation.SetPursuit(true);
                _headLook.Enable();
                return TaskResult.Running;
            }

            if (!_enemy.IsAngry)
                _walkAnimation.SetPursuit(false);

            return TaskResult.Failure;
        }
    }
}
