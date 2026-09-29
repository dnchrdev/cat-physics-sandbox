using Features.Enemy.Entities;
using Features.Enemy.Entities.Settings;
using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.UseCases.Tasks.Patrolling
{
    public sealed class TaskFindGroundedPatrolPoint : IEnemyTask
    {
        private readonly PatrolRoute _route;
        private readonly IPatrolPoints _patrolPoints;
        private readonly IGroundRaycaster _groundRaycaster;

        public TaskFindGroundedPatrolPoint(
            PatrolRoute route,
            IPatrolPoints patrolPoints,
            IGroundRaycaster groundRaycaster)
        {
            _route = route;
            _patrolPoints = patrolPoints;
            _groundRaycaster = groundRaycaster;
        }

        public TaskResult Evaluate(float dt)
        {
            var patrolPoint = _patrolPoints.GetPatrolPointPosition(_route.PointIndex);

            if (!_groundRaycaster.TryFindGround(patrolPoint, PatrolSettings.GroundSearchDistance, out var groundPoint))
                return TaskResult.Failure;

            _route.SetDestination(groundPoint);
            return TaskResult.Success;
        }
    }
}
