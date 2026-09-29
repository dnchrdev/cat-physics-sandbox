namespace Features.Enemy.Entities.Settings
{
    public sealed class KnockoutSettings
    {
        public float MinHitVelocity { get; }

        public float RestDuration { get; }

        public float AngerDuration { get; }

        public KnockoutSettings(float minHitVelocity, float restDuration, float angerDuration)
        {
            MinHitVelocity = minHitVelocity;
            RestDuration = restDuration;
            AngerDuration = angerDuration;
        }
    }
}
