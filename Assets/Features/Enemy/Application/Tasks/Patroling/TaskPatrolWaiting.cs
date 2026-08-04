using System.Collections;
using UnityEngine;

using Feature.BehaviourTree;

namespace Feature.EnemyFeature
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