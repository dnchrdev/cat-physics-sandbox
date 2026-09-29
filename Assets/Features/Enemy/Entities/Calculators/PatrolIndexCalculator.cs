namespace Features.Enemy.Entities.Calculators
{
    public static class PatrolIndexCalculator
    {
        public static int CalculateNextIndex(
            int currentIndex,
            int increment,
            bool isReversed,
            int pointsCount)
        {
            var nextIndex = currentIndex + increment * (isReversed ? -1 : 1);
            return ((nextIndex % pointsCount) + pointsCount) % pointsCount;
        }
    }
}
