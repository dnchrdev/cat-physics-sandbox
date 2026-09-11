using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks.Ragdoll
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