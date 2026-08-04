using System.Collections;
using UnityEngine;
using Feature.BehaviourTree;

namespace Feature.EnemyFeature
{
    public class TaskFindNewPatrolingIndex : EnemyTask
    {
        public TaskFindNewPatrolingIndex(EnemyContext ctx, Blackboard enemyBlackboard) : base(ctx, enemyBlackboard)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            if (Ctx.Config.IsRandomPatroling)
            {
                BB.CurrentPatrolingIndex = Ctx.PatrolPoints.GetRandomPatrolIndex();
            }
            else
            {
                if (Ctx.Config.IncludeReversedPatroling)
                {
                    if (BB.CurrentPatrolingReverseStrike < 0)
                    {
                        BB.CurrentPatrolingReverseStrike = Random.Range(Ctx.Config.PatrolingIndexMinReverseStrike, Ctx.Config.PatrolingIndexMaxReverseStrike + 1);
                        BB.IsReversePatroling = Random.Range(0f, 1f) > 0.5f? !BB.IsReversePatroling: BB.IsReversePatroling;
                    }
                    BB.CurrentPatrolingReverseStrike -= 1;
                }

                int incrementValue = Random.Range(Ctx.Config.PatrolingIndexMinIncrement, Ctx.Config.PatrolingIndexMaxIncrement + 1);
                BB.CurrentPatrolingIndex += incrementValue * (BB.IsReversePatroling ? -1 : 1);
                BB.CurrentPatrolingIndex =
                ((BB.CurrentPatrolingIndex % Ctx.PatrolPoints.PointsCount)
                + Ctx.PatrolPoints.PointsCount)
                % Ctx.PatrolPoints.PointsCount;
            }

            BB.PatrolingDelay = Random.Range(Ctx.Config.PatrolWaitMinDuration, Ctx.Config.PatrolWaitMaxDuration + 1f);

            return NodeState.SUCCESS;
        }
    }
}