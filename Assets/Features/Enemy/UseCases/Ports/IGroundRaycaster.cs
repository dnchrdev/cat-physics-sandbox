using UnityEngine;

namespace Features.Enemy.UseCases.Ports
{
    public interface IGroundRaycaster
    {
        bool TryFindGround(Vector3 origin, float maxDistance, out Vector3 groundPoint);
    }
}
