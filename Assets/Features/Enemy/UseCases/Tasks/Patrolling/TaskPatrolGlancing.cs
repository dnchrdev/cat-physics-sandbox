using Features.Enemy.Entities;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks.Patrolling
{
    public sealed class TaskPatrolGlancing : IEnemyTask
    {
        private readonly GlanceSchedule _schedule;
        private readonly HeadLook _headLook;
        private readonly PatrolSettings _settings;
        private readonly IRandom _random;

        public TaskPatrolGlancing(
            GlanceSchedule schedule,
            HeadLook headLook,
            PatrolSettings settings,
            IRandom random)
        {
            _schedule = schedule;
            _headLook = headLook;
            _settings = settings;
            _random = random;
        }

        public TaskResult Evaluate(float dt)
        {
            if (!_schedule.Tick(dt))
                return TaskResult.Success;

            if (_schedule.IsGlancing)
            {
                _schedule.SetPhaseDuration(_random.Range(_settings.GlanceMinDuration, _settings.GlanceMaxDuration));
                _headLook.Enable();
            }
            else
            {
                _schedule.SetPhaseDuration(_random.Range(_settings.GlanceMinDelay, _settings.GlanceMaxDelay));
                _headLook.Disable();
            }

            return TaskResult.Success;
        }
    }
}
