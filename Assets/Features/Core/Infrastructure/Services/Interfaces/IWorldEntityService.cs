using Features.Shared;
using UnityEngine;

namespace Features.Core.Infrastructure.Services.Interfaces
{
    public interface IWorldEntityService
    {
        IEntity GetFirstEntityByTeam(TeamType team);
        ITarget GetTargetByEntity(IEntity entity);
        GameObject GetObjectByEntity(IEntity entity);

        void Bind(IEntity entity, ITarget target, GameObject obj);
        void Unbind(IEntity entity);
    }
}