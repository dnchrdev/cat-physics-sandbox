namespace Features.Player.Entities
{
    public sealed class SurfaceProperties
    {
        public float WalkSpeed { get; }
        public float WalkMaxAccelerationResponse { get; }
        public float WalkMinAccelerationResponse { get; }
        public float AirSpeed { get; }
        public float AirAcceleration { get; }

        public SurfaceProperties(
            float walkSpeed,
            float walkMaxAccelerationResponse,
            float walkMinAccelerationResponse,
            float airSpeed,
            float airAcceleration)
        {
            WalkSpeed = walkSpeed;
            WalkMaxAccelerationResponse = walkMaxAccelerationResponse;
            WalkMinAccelerationResponse = walkMinAccelerationResponse;
            AirSpeed = airSpeed;
            AirAcceleration = airAcceleration;
        }
    }
}
