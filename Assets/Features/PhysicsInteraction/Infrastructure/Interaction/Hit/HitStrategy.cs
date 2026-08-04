using Feature.PhysicsInteraction;
using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
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