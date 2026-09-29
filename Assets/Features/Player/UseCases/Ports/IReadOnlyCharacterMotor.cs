using UnityEngine;

namespace Features.Player.UseCases.Ports
{
    public interface IReadOnlyCharacterMotor
    {
        Vector3 GetPosition();
        Quaternion GetRotation();
    }
}
