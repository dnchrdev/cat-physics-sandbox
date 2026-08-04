using Feature.Shared;
using UnityEngine;

namespace Feature.Core
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