using Features.Enemy.UseCases.Ports;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Enemy.InterfaceAdapters.Gateways
{
    public sealed class EnemyMoverGateway : IEnemyMover
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;

        public EnemyMoverGateway(NavMeshAgent agent)
        {
            _agent = agent;
            _transform = agent.transform;
        }

        public Vector3 Position => _transform.position;
        public Vector3 Forward => _transform.forward;
        public Quaternion Rotation => _transform.rotation;
        public Vector3 Destination => _agent.destination;

        public void MoveTo(Vector3 destination, float speed)
        {
            _agent.speed = speed;
            _agent.enabled = true;
            _agent.destination = destination;
        }

        public void Stop() => _agent.enabled = false;

        public void SetRotation(Quaternion rotation) => _transform.rotation = rotation;

        public void Teleport(Vector3 position) => _transform.position = position;
    }
}
