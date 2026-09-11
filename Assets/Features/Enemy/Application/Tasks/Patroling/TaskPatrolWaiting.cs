using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks.Patroling
{
    public class TaskPatrolWaiting : EnemyTask
    {
        public TaskPatrolWaiting(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            if (BB.PatrolingDelay < 0)
            {
                return NodeState.SUCCESS;
            }
            else
            {
                Ctx.MovableEnemy.Disable();
                Ctx.AnimatableEnemy.UpdateWalkSpeed(dt);
                BB.PatrolingDelay -= dt;
            }

            return NodeState.FAILURE;
        }
    }
}