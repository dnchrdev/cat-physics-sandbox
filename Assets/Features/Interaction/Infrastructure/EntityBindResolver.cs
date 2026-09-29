using Features.Core.Infrastructure;
using Features.Enemy.Infrastructure.Ragdoll;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.Infrastructure
{
    public sealed class EntityBindResolver
    {
        public ITarget ResolveAsTarget(Collider collider)
        {
            var bind = FindBind(collider);
            return bind != null ? bind.AsTarget : null;
        }

        public IEntity ResolveAsEntity(Collider collider)
        {
            var bind = FindBind(collider);
            return bind != null ? bind.AsEntity : null;
        }

        private static EntityWorldBind FindBind(Collider collider)
        {
            if (collider == null) return null;
            if (collider.TryGetComponent<EntityWorldBind>(out var bind)) return bind;
            return collider.TryGetComponent<RagdollBone>(out var bone) ? bone.EntityWorldBind : null;
        }
    }
}
