namespace Features.Enemy.Entities.Settings
{
    public sealed class WalkAnimationSettings
    {
        public float PatrolSpeed { get; }
        public float PursuitSpeed { get; }
        public float PatrolAnimationSpeed { get; }
        public float PursuitAnimationSpeed { get; }
        public float Response { get; }

        public WalkAnimationSettings(
            float patrolSpeed,
            float pursuitSpeed,
            float patrolAnimationSpeed,
            float pursuitAnimationSpeed,
            float response)
        {
            PatrolSpeed = patrolSpeed;
            PursuitSpeed = pursuitSpeed;
            PatrolAnimationSpeed = patrolAnimationSpeed;
            PursuitAnimationSpeed = pursuitAnimationSpeed;
            Response = response;
        }
    }
}
