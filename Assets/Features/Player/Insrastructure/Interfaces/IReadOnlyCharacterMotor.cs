using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface IReadOnlyCharacterMotor
    {
        public Vector3 GetPosition();
        public Quaternion GetRotation();
    }
}