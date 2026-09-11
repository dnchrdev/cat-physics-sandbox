using Features.PhysicsInteraction.Infrastructure.Interfaces;
using Features.Shared;
using UnityEngine;

namespace Features.PhysicsInteraction.Adapter.Interaction.ColliderHit
{
    public class ColliderHitStrategy : IColliderHit
    {
        private readonly EntityBindResolver _entityBindResolver;

        public ColliderHitStrategy(EntityBindResolver entityBindResolver)
        {
            _entityBindResolver = entityBindResolver;   
        }
        
        public void HandleHit(IEntity owner, Rigidbody ownerRb, Collision collision)
        {       
            var attackInfo = new AttackInfo(owner, false, ownerRb.linearVelocity.magnitude);
            var bind = _entityBindResolver.ResolveEntityBind(collision.collider);
            
            if(bind == null) return;
            
            bind.AsTarget.RecieveHit(attackInfo);
        }
    }
}