using System;

namespace Feature.PlayerFeature
{
    [Serializable]
    public class Surface
    {
        public string TagName;
        public float WalkSpeed;
        public float WalkMaxAccelerationResponse;

        public float WalkMinAccelerationResponse;

        //public float WalkDecelerationResponse;
        public float AirSpeed;
        public float AirAcceleration;
    }
}