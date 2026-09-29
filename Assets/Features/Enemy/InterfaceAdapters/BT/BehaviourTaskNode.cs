using Features.BehaviourTree.Entities;
using Features.Enemy.UseCases.Tasks;

namespace Features.Enemy.InterfaceAdapters.BT
{
    public sealed class BehaviourTaskNode : Node
    {
        private readonly IEnemyTask _task;

        public BehaviourTaskNode(IEnemyTask task)
        {
            _task = task;
        }

        public override NodeState Evaluate(float dt)
        {
            return _task.Evaluate(dt) switch
            {
                TaskResult.Running => NodeState.RUNNING,
                TaskResult.Success => NodeState.SUCCESS,
                _ => NodeState.FAILURE
            };
        }
    }
}
