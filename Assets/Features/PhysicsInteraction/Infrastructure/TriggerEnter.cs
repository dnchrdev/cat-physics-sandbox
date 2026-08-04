using System;
using System.Collections;
using UnityEngine;

namespace Feature.Shared
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