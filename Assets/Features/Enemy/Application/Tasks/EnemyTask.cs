using Feature.BehaviourTree;
using System.Collections;
using UnityEngine;

namespace Feature.EnemyFeature
{
    public class EnemyTask : Node
    {
        protected Blackboard BB;
        protected EnemyContext Ctx;

        public EnemyTask(EnemyContext ctx, Blackboard enemyBlackboard)
        {
            Ctx = ctx;
            BB = enemyBlackboard;
        }
    }
}