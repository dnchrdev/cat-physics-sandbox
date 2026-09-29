namespace Features.BehaviourTree.Entities
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public abstract class Node
    {
        public abstract NodeState Evaluate(float dt);

        public virtual void Reset()
        {
        }
    }
}
