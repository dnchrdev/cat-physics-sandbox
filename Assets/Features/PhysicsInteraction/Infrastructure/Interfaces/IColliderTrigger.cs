using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IColliderTrigger
    {
        void HandleTrigger(IEntity owner, Collider collider);
    }
}