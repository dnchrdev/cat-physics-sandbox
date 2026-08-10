using Feature.Core;
using Feature.EnemyFeature;
using Feature.PhysicsInteractio;
using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractableOwnership
    {
        private IEntity _owner;
        
        public void SetOwner(IEntity owner) => _owner = owner;
        
        public IEntity GetOwner() => _owner;
        
        public void ClearOwner() => _owner = null;
    }
}