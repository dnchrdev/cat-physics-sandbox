using UnityEngine;

namespace Features.Player.Insrastructure.Interfaces
{
    public interface ICharacterMotorReset
    {
        public void SetPosition(Vector3 newPos);
        public void SetRotation(Quaternion newRot);
    }
}