namespace Features.BehaviourTree.Entities
{
    public class Tree
    {
        private Node _root;

        public void Initialize(Node root)
        {
            _root?.Reset();
            _root = root;
        }

        public void Tick(float dt) => _root?.Evaluate(dt);

        public void Reset() => _root?.Reset();
    }
}
