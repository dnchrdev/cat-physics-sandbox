using Features.Shared;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Interfaces
{
    public interface IColliderTrigger
    {
        void HandleTrigger(IEntity owner, Collider collider);
    }
}