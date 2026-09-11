using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks
{
    public class TaskUpdateTicks : EnemyTask
    {
        public TaskUpdateTicks(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            Ctx.AttackAbility.Tick(dt);
            Ctx.AnimatableEnemy.UpdateWalkSpeed(dt);
            
            return NodeState.SUCCESS;
        }

    }
}