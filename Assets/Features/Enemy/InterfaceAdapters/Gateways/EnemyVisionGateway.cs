using Features.Enemy.Infrastructure.Configs;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.InterfaceAdapters.Gateways
{
    public sealed class EnemyVisionGateway : IEnemyVision
    {
        private readonly IEnemyHead _head;
        private readonly LayerMask _visionMask;

        public EnemyVisionGateway(IEnemyHead head, EnemyConfig config)
        {
            _head = head;
            _visionMask = config.VisionMask;
        }

        public bool CanSee(Vector3 point) =>
            !Physics.Linecast(_head.HeadPosition, point, out _, _visionMask);
    }
}
