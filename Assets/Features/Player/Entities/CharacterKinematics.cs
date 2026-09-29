using UnityEngine;

namespace Features.Player.Entities
{
    public struct CharacterKinematics
    {
        public bool Grounded;
        public bool Stable;
        public Vector3 Velocity;
        public Vector3 Acceleration;
    }
}
