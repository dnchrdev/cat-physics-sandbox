using Features.Enemy.Entities;
using Features.Enemy.Entities.Calculators;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks.Patrolling
{
    public sealed class TaskPatrolingToPoint : IEnemyTask
    {
        private readonly PatrolRoute _route;
        private readonly PatrolSettings _settings;
        private readonly IEnemyMover _mover;

        public TaskPatrolingToPoint(PatrolRoute route, PatrolSettings settings, IEnemyMover mover)
        {
            _route = route;
            _settings = settings;
            _mover = mover;
        }

        public TaskResult Evaluate(float dt)
        {
            _mover.MoveTo(_route.Destination, _settings.Speed);

            var isReached = DestinationDistanceCalculator.IsDestinationReached(
                _mover.Position,
                _mover.Destination,
                _settings.StoppingDistance);

            return isReached ? TaskResult.Success : TaskResult.Running;
        }
    }
}
