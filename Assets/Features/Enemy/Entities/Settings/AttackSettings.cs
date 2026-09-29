namespace Features.Enemy.Entities.Settings
{
    public sealed class AttackSettings
    {
        public float StartMaxYawAngle { get; }
        public float DamageMaxYawAngle { get; }
        public float RotationSpeed { get; }
        public float Cooldown { get; }
        public float SphereDistance { get; }
        public float HorizontalDistance { get; }

        public AttackSettings(
            float startMaxYawAngle,
            float damageMaxYawAngle,
            float rotationSpeed,
            float cooldown,
            float sphereDistance,
            float horizontalDistance)
        {
            StartMaxYawAngle = startMaxYawAngle;
            DamageMaxYawAngle = damageMaxYawAngle;
            RotationSpeed = rotationSpeed;
            Cooldown = cooldown;
            SphereDistance = sphereDistance;
            HorizontalDistance = horizontalDistance;
        }
    }
}
