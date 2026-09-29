using Features.Enemy.Infrastructure.Configs;
using Features.Enemy.UseCases.Ports;
using UnityEngine;

namespace Features.Enemy.InterfaceAdapters.Gateways
{
    public sealed class GroundRaycastGateway : IGroundRaycaster
    {
        private readonly LayerMask _groundMask;

        public GroundRaycastGateway(EnemyConfig config)
        {
            _groundMask = config.GroundMask;
        }

        public bool TryFindGround(Vector3 origin, float maxDistance, out Vector3 groundPoint)
        {
            if (Physics.Linecast(origin, origin + Vector3.down * maxDistance, out var hit, _groundMask))
            {
                groundPoint = hit.point;
                return true;
            }

            groundPoint = default;
            return false;
        }
    }
}
