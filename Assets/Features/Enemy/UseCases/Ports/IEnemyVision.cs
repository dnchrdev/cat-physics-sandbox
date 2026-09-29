using UnityEngine;

namespace Features.Enemy.UseCases.Ports
{
    public interface IEnemyVision
    {
        bool CanSee(Vector3 point);
    }
}
