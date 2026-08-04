using System.Collections.Generic;
using UnityEngine;

namespace Feature.BehaviourTree
{
    public class Selector : Node
    {
        public Selector() : base() { }

        public Selector(List<Node> children) : base(children) { }

        public override NodeState Evaluate(float dt)
        {
            foreach (var child in Children)
            {
                switch (child.Evaluate(dt))
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        State = NodeState.SUCCESS;
                        return State;
                    case NodeState.RUNNING:
                        State = NodeState.RUNNING;
                        return State;
                    default:
                        continue;
                }
            }
            State = NodeState.FAILURE;
            return State;
        }
    }
}