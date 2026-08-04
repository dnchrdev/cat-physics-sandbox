using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IColliderHit
    {
        void HandleHit(IEntity owner, Rigidbody ownerRb, Collision collision);
    }
}