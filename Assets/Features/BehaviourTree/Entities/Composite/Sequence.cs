using System.Collections.Generic;

namespace Features.BehaviourTree.Entities.Composite
{
    public class Sequence : CompositeNode
    {
        private int _currentIndex;

        public Sequence(IEnumerable<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate(float dt)
        {
            while (_currentIndex < Children.Count)
            {
                switch (Children[_currentIndex].Evaluate(dt))
                {
                    case NodeState.RUNNING:
                        return NodeState.RUNNING;
                    case NodeState.FAILURE:
                        Reset();
                        return NodeState.FAILURE;
                    default:
                        _currentIndex++;
                        break;
                }
            }

            Reset();
            return NodeState.SUCCESS;
        }

        public override void Reset()
        {
            _currentIndex = 0;
            ResetChildren();
        }
    }
}
