using System;
using UnityEngine;

namespace Feature.Shared
{
    public class HitEnter : MonoBehaviour
    {
        public event Action<Collision> OnHitEvent;

        private void OnCollisionEnter(Collision collision)
        {
            OnHitEvent?.Invoke(collision);
        }
    }
}