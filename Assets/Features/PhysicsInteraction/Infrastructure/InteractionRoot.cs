using UnityEngine;
using Zenject;

namespace Feature.PhysicsInteraction
{
    public class InteractionRoot : MonoBehaviour        
    {
        private Rigidbody _rb;

        [Inject]
        private void Construct()
        {

        }

        void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public void SetPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        public void SetRotation(Quaternion newRot)
        {
            transform.rotation = newRot;
        }

        public Vector3 GetVelocity()
        {
            return _rb.linearVelocity;
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rb.linearVelocity = velocity;
        }

        public void SetAngularVelocity(Vector3 angularVelocity)
        {
            _rb.angularVelocity = angularVelocity;
        }
    }
}