using System.Collections.Generic;

namespace Feature.BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence() : base() { }

        public Sequence(List<Node> children) : base(children) {}

        public override NodeState Evaluate(float dt)
        {
            bool anyChildIsRunning = false;

            foreach (var child in Children)
            {
                switch (child.Evaluate(dt))
                {
                    case NodeState.FAILURE:
                        State = NodeState.FAILURE;
                        return State;
                    case NodeState.SUCCESS:
                        continue;
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        continue;
                    default:
                        State = NodeState.SUCCESS;
                        return State;
                }
            }

            State = anyChildIsRunning ? NodeState.RUNNING : NodeState.SUCCESS;
            return State;
        }
    }
}