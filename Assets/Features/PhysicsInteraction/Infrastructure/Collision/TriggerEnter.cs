using System;
using UnityEngine;

namespace Features.PhysicsInteraction.Infrastructure.Collision
{
    public class TriggerEnter : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEnterEvent;
        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}