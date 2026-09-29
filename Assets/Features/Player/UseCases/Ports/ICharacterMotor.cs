using Features.Player.Entities;
using UnityEngine;

namespace Features.Player.UseCases.Ports
{
    public interface ICharacterMotor : IReadOnlyCharacterMotor
    {
        Vector3 GetVelocity();
        void SetVelocity(Vector3 velocity);
        
        GroundProbe ProbeGround(bool withStepDown);

        void Teleport(Vector3 position, Quaternion rotation);
    }
}
