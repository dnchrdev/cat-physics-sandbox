using System;

namespace Features.Player.Insrastructure.Data
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