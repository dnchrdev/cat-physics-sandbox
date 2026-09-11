using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks.Attack
{
    public class TaskAttacking : EnemyTask
    {
        public TaskAttacking(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            if (!BB.IsAngry && !Ctx.AttackAbility.IsPlaying)
                return NodeState.FAILURE;

            if (Ctx.AttackAbility.IsPlaying)
            {
                Ctx.MovableEnemy.Disable();
                Ctx.AnimatableEnemy.UpdateWalkSpeed(dt);
                Ctx.AnimatableEnemy.SetPursuit(false);
                return NodeState.RUNNING;
            }

            if (Ctx.Enemy.TargetPlayer.IsAlive && TryAttack(dt))
                return NodeState.RUNNING;

            return NodeState.FAILURE;
        }

        private bool TryAttack(float dt)
        {
            if (Ctx.AttackAbility.CanAttack && 
                Ctx.AttackAbility.CanHitTarget() && 
                Ctx.EnemyVisionAndLook.IsPlayerVisible())
            {
                if (Ctx.AttackAbility.IsAttackAngleValid(Ctx.Config.AttackStartMaxYawAngle))
                {
                    Ctx.AttackAbility.Attack();
                }
                
                Ctx.AttackAbility.UpdateAttackRotation(dt);
                return true;
            }

            return false;
        }
        
    }
}