namespace Features.Shared.Utilities
{
    public static class MathExtensions
    {
        public static float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
    }
}
