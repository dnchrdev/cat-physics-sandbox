using System.Collections.Generic;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Core.InterfaceAdapters.Services
{
    public sealed class WorldEnemyHeadRegistry : IWorldEnemyHeadRegistry
    {
        private readonly Dictionary<IEntity, Transform> _headsByEntity = new();
        
        public void Bind(IEntity entity, Transform head)
        {
            _headsByEntity[entity] = head;
        }
        
        public Transform GetEnemyHeadByEntity(IEntity entity)
        {
            return _headsByEntity.TryGetValue(entity, out var head) ? head : null;
        }

        public void Unbind(IEntity entity)
        {
            _headsByEntity.Remove(entity);
        }
    }
}
