using Features.Enemy.Entities;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks.Patrolling
{
    public sealed class TaskPatrolWaiting : IEnemyTask
    {
        private readonly PatrolRoute _route;
        private readonly IEnemyMover _mover;
        private readonly WalkAnimationUseCase _walkAnimation;

        public TaskPatrolWaiting(PatrolRoute route, IEnemyMover mover, WalkAnimationUseCase walkAnimation)
        {
            _route = route;
            _mover = mover;
            _walkAnimation = walkAnimation;
        }

        public TaskResult Evaluate(float dt)
        {
            if (_route.IsWaitOver)
                return TaskResult.Success;

            _mover.Stop();
            _route.Wait(dt);
            return TaskResult.Running;
        }
    }
}
