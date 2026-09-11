using Features.PhysicsInteraction.Infrastructure.Interfaces;
using UnityEngine;

namespace Features.PhysicsInteraction.Adapter.Interaction.Hit
{
    public class HitStrategy : IHitable
    {
        public void Hit(Vector3 direction, Vector3 atPoint, float power, Rigidbody rb)
        {
            rb.isKinematic = false;
            rb.AddForceAtPosition(direction * power, atPoint, ForceMode.Impulse);
        }
    }
}