using Features.Shared;

namespace Features.PhysicsInteraction.Adapter.Interaction.InteractableComponents
{
    public class InteractableOwnership
    {
        private IEntity _owner;
        
        public void SetOwner(IEntity owner) => _owner = owner;
        
        public IEntity GetOwner() => _owner;
        
        public void ClearOwner() => _owner = null;
    }
}