using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IHitable
    {
        void Hit(Vector3 direction, Vector3 atPoint, float power, Rigidbody rb);
    }
}