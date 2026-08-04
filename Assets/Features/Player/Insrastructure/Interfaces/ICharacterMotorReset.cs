using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface ICharacterMotorReset
    {
        public void SetPosition(Vector3 newPos);
        public void SetRotation(Quaternion newRot);
    }
}