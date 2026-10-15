using System.Collections.Generic;

namespace Features.BehaviourTree.Entities.Composite
{
    public abstract class CompositeNode : Node
    {
        protected readonly List<Node> Children;

        protected CompositeNode(IEnumerable<Node> children)
        {
            Children = new List<Node>(children);
        }
    
        protected void ResetChildren()
        {
            foreach (var child in Children)
                child.Reset();
        }
    }
}
