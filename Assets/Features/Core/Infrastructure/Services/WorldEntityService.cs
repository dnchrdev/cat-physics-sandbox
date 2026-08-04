using Feature.Shared;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.Core
{
    public class WorldEntityService : IWorldEntityService
    {
        private readonly Dictionary<IEntity, GameObject> _entities = new();
        private readonly Dictionary<IEntity, ITarget> _targets = new();

        public void Bind(IEntity entity, ITarget target, GameObject obj)
        {
            _entities.Add(entity, obj);
            _targets.Add(entity, target);
        }

        public IEntity GetFirstEntityByTeam(TeamType team)
        {
            foreach (var entity in _entities)
            {
                if (entity.Key.Team == team)
                    return entity.Key;
            }

            return null;
        }

        public GameObject GetObjectByEntity(IEntity byEntity)
        {
            foreach (var entity in _entities)
            {
                if (entity.Key == byEntity)
                    return entity.Value;
            }

            return null;
        }

        public ITarget GetTargetByEntity(IEntity entity)
        {
            if (_targets.TryGetValue(entity, out var target))
            {
                return target;
            }

            return null;
        }

        public void Unbind(IEntity entity)
        {
            _entities.Remove(entity);
            _targets.Remove(entity);
        }
    }
}