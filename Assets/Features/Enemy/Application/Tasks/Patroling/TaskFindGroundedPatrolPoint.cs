using Feature.BehaviourTree;
using UnityEngine;

namespace Feature.EnemyFeature
{
    public class TaskFindGroundedPatrolPoint : EnemyTask
    {
        public TaskFindGroundedPatrolPoint(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {

            BB.TimerHeadLooking -= dt;

            if (BB.TimerHeadLooking <= 0f)
            {
                BB.IsHeadLooking = !BB.IsHeadLooking;

                if (BB.IsHeadLooking)
                {
                    BB.TimerHeadLooking = Random.Range(Ctx.Config.PatrolingHeadLookMinSeconds, Ctx.Config.PatrolingHeadLookMaxSeconds);
                    Ctx.EnemyVisionAndLook.EnableHeadLooking();
                }
                else
                {
                    BB.TimerHeadLooking = Random.Range(Ctx.Config.PatrolingHeadLookMinSecondsDelay, Ctx.Config.PatrolingHeadLookMaxSecondsDelay);
                    Ctx.EnemyVisionAndLook.DisableHeadLooking();
                }
            }

            var patrolPoint = Ctx.PatrolPoints.GetPatrolPointPosition(BB.CurrentPatrolingIndex);

            if (Physics.Linecast(patrolPoint, patrolPoint + Vector3.down * 25f, out var hitInfo, Ctx.Config.GroundMask))
            {
                BB.CurrentPatrolPosition = hitInfo.point;
                return NodeState.SUCCESS;
            }

            return NodeState.FAILURE;

        }
    }
}