using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Camera.UseCases.Ports
{
    public interface IEnemyHeadPositionProvider
    {
        Vector3? GetHeadPosition(IEntity entity);
    }
}
