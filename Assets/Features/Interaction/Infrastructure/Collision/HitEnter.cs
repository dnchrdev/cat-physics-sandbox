using System;
using UnityEngine;

namespace Features.Interaction.Infrastructure.Collision
{
    public sealed class HitEnter : MonoBehaviour
    {
        public event Action<UnityEngine.Collision> OnHitEvent;

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            OnHitEvent?.Invoke(collision);
        }
    }
}