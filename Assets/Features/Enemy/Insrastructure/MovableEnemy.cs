using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Feature.EnemyFeature
{
    public class MovableEnemy: IMovableEnemy
    {
        [Inject] private readonly NavMeshAgent _agent;
        public void Enable()
        {
            _agent.enabled = true;
        }

        public void Disable()
        {
            _agent.enabled = false;
        }

        public Vector3 GetForward()
        {
            return _agent.transform.forward;
        }

        public Vector3 GetPosition()
        {
            return _agent.transform.position;
        }

        public void SetPosition(Vector3 newPosition)
        {
            _agent.transform.position  = newPosition;
        }

        public Quaternion GetRotation()
        {
            return _agent.transform.rotation;
        }

        public void SetRotation(Quaternion newRotation)
        {
            _agent.transform.rotation =  newRotation;
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.destination = destination;
        }

        public Vector3 GetDestination()
        {
            return _agent.destination;
        }

        public void SetSpeed(float speed)
        {
            _agent.speed = speed;
        }
    }
}