using UnityEngine;

namespace Features.Enemy.UseCases.Ports
{
    public interface IPatrolPoints
    {
        int PointsCount { get; }
        Vector3 GetPatrolPointPosition(int index);
        int GetNearestPatrolIndex(Vector3 position);
    }
}
