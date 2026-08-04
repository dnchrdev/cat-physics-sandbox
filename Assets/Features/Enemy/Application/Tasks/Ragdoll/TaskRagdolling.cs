using System.Collections;
using UnityEngine;
using Feature.BehaviourTree;

namespace Feature.EnemyFeature
{
    public class TaskRagdolling : EnemyTask
    {
        public TaskRagdolling(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            if (BB.IsRagdoll)
            {
                Ctx.MovableEnemy.Disable();
                Ctx.AnimatableEnemy.SetPursuit(false);
                Ctx.EnemyVisionAndLook.DisableHeadLooking();
                return NodeState.SUCCESS;
            }
            //Ctx.Animator.enabled = true;
            return NodeState.FAILURE;
        }
    }
}