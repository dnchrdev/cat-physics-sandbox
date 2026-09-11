using Features.Shared;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Interfaces
{
    public interface IColliderHit
    {
        void HandleHit(IEntity owner, Rigidbody ownerRb, UnityEngine.Collision collision);
    }
}