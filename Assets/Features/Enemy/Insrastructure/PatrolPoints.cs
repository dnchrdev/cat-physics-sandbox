using System;
using UnityEngine;
using UnityEngine.Splines;

namespace Feature.EnemyFeature
{
    public class PatrolPoints : MonoBehaviour
    {
        [SerializeField] private SplineContainer _splineContainer;

        public int PointsCount => _splineContainer.Splines[0].Count;

        public Vector3 GetPatrolPointPosition(int nextIndex)
        {
            if(PointsCount == 0) throw new Exception("There is no patrol points");
            if (nextIndex < 0 || nextIndex > PointsCount) throw new Exception("Invalid patrol index");

            var spline = _splineContainer.Splines[0];
            BezierKnot knot = spline[nextIndex];
            Vector3 worldPos = _splineContainer.transform.TransformPoint(knot.Position);
            return worldPos;
        }

        public int GetRandomPatrolIndex()
        {
            int nextIndex = UnityEngine.Random.Range(0, PointsCount);

            return nextIndex;
        }

        public int GetNearestPatrolIndex(Vector3 position)
        {
            var spline = _splineContainer.Splines[0];
            int nearestIndex = 0;
            float nearestDistance = float.MaxValue;

            for (int i = 0; i < PointsCount; i++)
            {
                Vector3 worldPos = _splineContainer.transform.TransformPoint(spline[i].Position);
                float distance = Vector3.Distance(position, worldPos);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestIndex = i;
                }
            }

            return nearestIndex;
        }
    }
}