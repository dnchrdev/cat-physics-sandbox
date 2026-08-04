using System.Collections;
using UnityEngine;
using Feature.BehaviourTree;

namespace Feature.EnemyFeature
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