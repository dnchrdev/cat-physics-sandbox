using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using UnityEngine;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks.Patroling
{
    public class TaskPatrolingToPoint : EnemyTask
    {
        public TaskPatrolingToPoint(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            Ctx.MovableEnemy.SetSpeed(Ctx.Config.PatrolSpeed);
            Ctx.MovableEnemy.Enable();
            Ctx.MovableEnemy.SetDestination(BB.CurrentPatrolPosition);

            var horizontalDeltaToDestination = Vector3.ProjectOnPlane(Ctx.MovableEnemy.GetDestination() - Ctx.MovableEnemy.GetPosition(), Vector3.up);

            var horizontalDistance = horizontalDeltaToDestination.magnitude;

            if (horizontalDistance <= Ctx.Config.PatrolStoppingDistance)
            {
                return NodeState.SUCCESS;
            }

            return NodeState.FAILURE;
        }

    }
}