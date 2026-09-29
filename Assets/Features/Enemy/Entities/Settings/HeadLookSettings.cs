namespace Features.Enemy.Entities.Settings
{
    public sealed class HeadLookSettings
    {
        public float YawMaxAngle { get; }
        public float PitchMaxAngle { get; }
        public float Response { get; }

        public HeadLookSettings(float yawMaxAngle, float pitchMaxAngle, float response)
        {
            YawMaxAngle = yawMaxAngle;
            PitchMaxAngle = pitchMaxAngle;
            Response = response;
        }
    }
}
