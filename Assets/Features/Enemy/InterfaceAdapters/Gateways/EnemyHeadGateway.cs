using Features.Enemy.Infrastructure.Rig;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.InterfaceAdapters.Gateways
{
    public sealed class EnemyHeadGateway : IEnemyHead
    {
        private readonly Transform _head;

        public EnemyHeadGateway(EnemyRefs refs)
        {
            _head = refs.Head;
        }

        public Vector3 HeadPosition => _head.position;
    }
}
