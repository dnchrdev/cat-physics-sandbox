using System;
using Features.Enemy.UseCases.Ports;
using UnityEngine;
using UnityEngine.Splines;

namespace Features.Enemy.Infrastructure.Rig
{
    public class PatrolPoints : MonoBehaviour, IPatrolPoints
    {
        [SerializeField] private SplineContainer _splineContainer;

        private Spline Spline => _splineContainer.Splines[0];

        public int PointsCount => Spline.Count;

        public Vector3 GetPatrolPointPosition(int index)
        {
            if (PointsCount == 0)
                throw new Exception($"{name}: there are no patrol points.");
            if (index < 0 || index >= PointsCount)
                throw new ArgumentOutOfRangeException(nameof(index), index, $"{name}: invalid patrol index.");

            return KnotWorldPosition(index);
        }

        public int GetNearestPatrolIndex(Vector3 position)
        {
            var nearestIndex = 0;
            var nearestDistance = float.MaxValue;

            for (var i = 0; i < PointsCount; i++)
            {
                var distance = Vector3.Distance(position, KnotWorldPosition(i));
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }

        private Vector3 KnotWorldPosition(int index) =>
            _splineContainer.transform.TransformPoint(Spline[index].Position);
    }
}
