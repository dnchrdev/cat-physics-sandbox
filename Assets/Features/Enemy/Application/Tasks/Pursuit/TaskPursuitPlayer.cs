using System.Collections;
using UnityEngine;
using Feature.BehaviourTree;

namespace Feature.EnemyFeature
{
    public class TaskPursuitPlayer : EnemyTask
    {
        public TaskPursuitPlayer(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            if (BB.IsAngry && Ctx.Player.IsAlive)
            {
                Ctx.MovableEnemy.SetSpeed(Ctx.Config.PursuitSpeed);
                Ctx.MovableEnemy.Enable();
                Ctx.MovableEnemy.SetDestination(Ctx.Player.Position);
                Ctx.AnimatableEnemy.SetPursuit(true);
                Ctx.EnemyVisionAndLook.EnableHeadLooking();
                return NodeState.RUNNING;
            }
            
            if(BB.IsAngry == false) Ctx.AnimatableEnemy.SetPursuit(false); 
            
            return NodeState.FAILURE;
            
        }

    }
}