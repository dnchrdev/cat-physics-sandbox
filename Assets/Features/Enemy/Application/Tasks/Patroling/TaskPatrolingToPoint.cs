using Feature.BehaviourTree;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Feature.EnemyFeature
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