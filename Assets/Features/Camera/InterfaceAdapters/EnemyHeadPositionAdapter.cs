using Features.Camera.UseCases.Ports;
using Features.Core.InterfaceAdapters;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Camera.InterfaceAdapters
{
    public class EnemyHeadPositionAdapter : IEnemyHeadPositionProvider
    {
        private readonly IWorldEnemyHeadRegistry _worldEnemyHeadRegistry;

        public EnemyHeadPositionAdapter(IWorldEnemyHeadRegistry worldEnemyHeadRegistry)
        {
            _worldEnemyHeadRegistry = worldEnemyHeadRegistry;
        }

        public Vector3? GetHeadPosition(IEntity entity)
        {
            var head = _worldEnemyHeadRegistry.GetEnemyHeadByEntity(entity);
            if (head == null) return null;
            
            return head.position;
        }
    }
}
