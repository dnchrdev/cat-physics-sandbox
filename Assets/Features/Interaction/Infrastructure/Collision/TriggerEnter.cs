using System;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Collision
{
    public sealed class TriggerEnter : MonoBehaviour
    {
        public event Action<Collider> OnTriggerEnterEvent;

        private void OnTriggerEnter(Collider other)
        {
            OnTriggerEnterEvent?.Invoke(other);
        }
    }
}