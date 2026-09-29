using Features.Enemy.Entities;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks.Patrolling
{
    public sealed class TaskFindNewPatrolingIndex : IEnemyTask
    {
        private readonly PatrolRoute _route;
        private readonly PatrolSettings _settings;
        private readonly IPatrolPoints _patrolPoints;
        private readonly IRandom _random;

        public TaskFindNewPatrolingIndex(
            PatrolRoute route,
            PatrolSettings settings,
            IPatrolPoints patrolPoints,
            IRandom random)
        {
            _route = route;
            _settings = settings;
            _patrolPoints = patrolPoints;
            _random = random;
        }

        public TaskResult Evaluate(float dt)
        {
            if (_settings.IsRandom)
            {
                var index = _random.Range(0, _patrolPoints.PointsCount);
                _route.JumpTo(index, NextWaitTime());
                return TaskResult.Success;
            }

            if (_settings.IncludeReversed)
            {
                if (_route.IsTimeToRollDirection)
                {
                    var steps = _random.Range(_settings.MinReverseStrike, _settings.MaxReverseStrike + 1);
                    var flip = _random.Range(0f, 1f) > 0.5f;
                    _route.RollDirection(steps, flip);
                }

                _route.CountDirectionStep();
            }

            var increment = _random.Range(_settings.MinIncrement, _settings.MaxIncrement + 1);
            _route.Advance(increment, _patrolPoints.PointsCount, NextWaitTime());
            return TaskResult.Success;
        }

        private float NextWaitTime() =>
            _random.Range(_settings.WaitMinDuration, _settings.WaitMaxDuration + 1f);
    }
}
