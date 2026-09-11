using Features.BehaviourTree;
using Features.Enemy.Insrastructure;
using Blackboard = Features.Enemy.Domain.Blackboard;

namespace Features.Enemy.Application.Tasks
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