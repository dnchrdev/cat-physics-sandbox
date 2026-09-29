using System;
using Features.Player.Entities;

namespace Features.Player.Infrastructure.Configs
{
    [Serializable]
    public class SurfaceData
    {
        public string TagName;
        public float WalkSpeed;
        public float WalkMaxAccelerationResponse;
        public float WalkMinAccelerationResponse;
        public float AirSpeed;
        public float AirAcceleration;

        public SurfaceProperties ToProperties() => new SurfaceProperties(
            WalkSpeed,
            WalkMaxAccelerationResponse,
            WalkMinAccelerationResponse,
            AirSpeed,
            AirAcceleration);
    }
}
