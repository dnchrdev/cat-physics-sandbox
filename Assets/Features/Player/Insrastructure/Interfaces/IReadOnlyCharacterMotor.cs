using UnityEngine;

namespace Features.Player.Insrastructure.Interfaces
{
    public interface IReadOnlyCharacterMotor
    {
        public Vector3 GetPosition();
        public Quaternion GetRotation();
    }
}