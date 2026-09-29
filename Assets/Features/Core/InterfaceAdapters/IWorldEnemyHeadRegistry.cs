using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Core.InterfaceAdapters
{
    public interface IWorldEnemyHeadRegistry
    {
        void Bind(IEntity entity, Transform head);
        Transform GetEnemyHeadByEntity(IEntity entity);
        void Unbind(IEntity entity);
    }
}
