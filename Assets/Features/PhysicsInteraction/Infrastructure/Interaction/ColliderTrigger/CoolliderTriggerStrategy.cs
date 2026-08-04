using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class CoolliderTriggerStrategy : IColliderTrigger
    {
        private readonly EntityBindResolver _entityBindResolver;

        public CoolliderTriggerStrategy(EntityBindResolver entityBindResolver)
        {
            _entityBindResolver = entityBindResolver;   
        }
        
        public void HandleTrigger(IEntity owner, Collider collider)
        {
            var attackInfo = new AttackInfo(owner, true);
            var bind = _entityBindResolver.ResolveEntityBind(collider);
            
            if(bind == null) return;
            
            bind.AsTarget.RecieveHit(attackInfo);
        }
    }
}