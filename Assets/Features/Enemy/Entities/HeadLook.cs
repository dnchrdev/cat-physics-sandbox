using Features.Enemy.Entities.Calculators;

namespace Features.Enemy.Entities
{
    public sealed class HeadLook
    {
        public bool IsEnabled { get; private set; }
        public float Weight { get; private set; }

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;

        public void UpdateWeight(bool isWithinAngle, bool isTargetVisible, float response, float dt)
        {
            Weight = HeadLookCalculator.CalculateSmoothedWeight(
                Weight, isWithinAngle, IsEnabled, isTargetVisible, response, dt);
        }
    }
}
