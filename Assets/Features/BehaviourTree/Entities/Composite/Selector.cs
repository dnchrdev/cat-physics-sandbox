using System.Collections.Generic;

namespace Features.BehaviourTree.Entities.Composite
{
    public class Selector : CompositeNode
    {
        private Node _runningChild;

        public Selector(IEnumerable<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            foreach (var child in Children)
            {
                var state = child.Evaluate(dt);
                if (state == NodeState.FAILURE)
                    continue;

                if (_runningChild != null && _runningChild != child)
                    _runningChild.Reset();

                _runningChild = state == NodeState.RUNNING ? child : null;
                
                return state;
            }

            Reset();
            return NodeState.FAILURE;
        }

        public override void Reset()
        {
            _runningChild?.Reset();
            _runningChild = null;
        }
    }
}
