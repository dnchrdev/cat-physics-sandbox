using System;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Collision
{
    public class HitEnter : MonoBehaviour
    {
        public event Action<UnityEngine.Collision> OnHitEvent;

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            OnHitEvent?.Invoke(collision);
        }
    }
}