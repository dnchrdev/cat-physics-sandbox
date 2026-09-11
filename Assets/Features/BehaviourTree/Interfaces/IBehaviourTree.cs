namespace Features.BehaviourTree.Interfaces
{ 
    public interface IBehaviourTree 
    {
        void Initialize(Node root);
        void Tick(float dt);
    }
}