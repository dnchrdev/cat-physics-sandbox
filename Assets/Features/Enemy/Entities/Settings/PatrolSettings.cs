namespace Features.Enemy.Entities.Settings
{
    public sealed class PatrolSettings
    {
        public const float GroundSearchDistance = 25f;

        public float Speed { get; }
        public float StoppingDistance { get; }
        public float WaitMinDuration { get; }
        public float WaitMaxDuration { get; }
        public bool IsRandom { get; }
        public bool IncludeReversed { get; }
        public int MinIncrement { get; }
        public int MaxIncrement { get; }
        public int MinReverseStrike { get; }
        public int MaxReverseStrike { get; }
        public float GlanceMinDuration { get; }
        public float GlanceMaxDuration { get; }
        public float GlanceMinDelay { get; }
        public float GlanceMaxDelay { get; }

        public PatrolSettings(
            float speed,
            float stoppingDistance,
            float waitMinDuration,
            float waitMaxDuration,
            bool isRandom,
            bool includeReversed,
            int minIncrement,
            int maxIncrement,
            int minReverseStrike,
            int maxReverseStrike,
            float glanceMinDuration,
            float glanceMaxDuration,
            float glanceMinDelay,
            float glanceMaxDelay)
        {
            Speed = speed;
            StoppingDistance = stoppingDistance;
            WaitMinDuration = waitMinDuration;
            WaitMaxDuration = waitMaxDuration;
            IsRandom = isRandom;
            IncludeReversed = includeReversed;
            MinIncrement = minIncrement;
            MaxIncrement = maxIncrement;
            MinReverseStrike = minReverseStrike;
            MaxReverseStrike = maxReverseStrike;
            GlanceMinDuration = glanceMinDuration;
            GlanceMaxDuration = glanceMaxDuration;
            GlanceMinDelay = glanceMinDelay;
            GlanceMaxDelay = glanceMaxDelay;
        }
    }
}
