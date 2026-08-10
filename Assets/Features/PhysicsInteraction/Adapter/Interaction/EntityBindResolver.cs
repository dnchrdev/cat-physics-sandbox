using Feature.Core;
using Feature.EnemyFeature;
using UnityEngine;

namespace Feature.PhysicsInteraction
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