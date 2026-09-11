using Features.Core.Infrastructure.Binder;
using Features.Enemy.Insrastructure.Ragdoll;
using UnityEngine;

namespace Features.PhysicsInteraction.Adapter.Interaction
{
    public class EntityBindResolver
    {
        public EntityWorldBind ResolveEntityBind(Collider collider)
        {
            var bind = collider.GetComponent<EntityWorldBind>();
            
            if (bind != null) return bind;

            if (collider.TryGetComponent<RagdollBone>(out var ragdoll))
                return ragdoll.EntityWorldBind;

            return null;
        }
    }
}