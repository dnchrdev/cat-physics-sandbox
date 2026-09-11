using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Interfaces
{
    public interface IHitable
    {
        void Hit(Vector3 direction, Vector3 atPoint, float power, Rigidbody rb);
    }
}