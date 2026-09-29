using Features.Enemy.Entities.Calculators;
using UnityEngine;

namespace Features.Enemy.Entities
{
    public sealed class PatrolRoute
    {
        private int _stepsUntilDirectionRoll;

        public int PointIndex { get; private set; }
        public bool IsReversed { get; private set; }

        public Vector3 Destination { get; private set; }

        public float WaitTimeLeft { get; private set; }
        public bool IsWaitOver => WaitTimeLeft < 0f;
        public bool IsTimeToRollDirection => _stepsUntilDirectionRoll < 0;

        public void SetDestination(Vector3 groundPoint) => Destination = groundPoint;

        public void Wait(float dt) => WaitTimeLeft -= dt;

        public void JumpTo(int pointIndex, float waitTime)
        {
            PointIndex = pointIndex;
            WaitTimeLeft = waitTime;
        }

        public void Advance(int increment, int pointsCount, float waitTime)
        {
            PointIndex = PatrolIndexCalculator.CalculateNextIndex(PointIndex, increment, IsReversed, pointsCount);
            WaitTimeLeft = waitTime;
        }

        public void RollDirection(int steps, bool flip)
        {
            _stepsUntilDirectionRoll = steps;
            if (flip)
                IsReversed = !IsReversed;
        }

        public void CountDirectionStep() => _stepsUntilDirectionRoll -= 1;
    }
}
