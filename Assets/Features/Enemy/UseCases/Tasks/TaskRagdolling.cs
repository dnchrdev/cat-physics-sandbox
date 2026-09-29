using Features.Enemy.Entities;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks
{
    public sealed class TaskRagdolling : IEnemyTask
    {
        private readonly Entities.Enemy _enemy;
        private readonly HeadLook _headLook;
        private readonly IEnemyMover _mover;
        private readonly WalkAnimationUseCase _walkAnimation;

        public TaskRagdolling(
            Entities.Enemy enemy,
            HeadLook headLook,
            IEnemyMover mover,
            WalkAnimationUseCase walkAnimation)
        {
            _enemy = enemy;
            _headLook = headLook;
            _mover = mover;
            _walkAnimation = walkAnimation;
        }

        public TaskResult Evaluate(float dt)
        {
            if (!_enemy.IsKnockedOut)
                return TaskResult.Failure;

            _mover.Stop();
            _walkAnimation.SetPursuit(false);
            _headLook.Disable();
            return TaskResult.Success;
        }
    }
}
