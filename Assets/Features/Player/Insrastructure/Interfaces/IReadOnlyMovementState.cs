using UnityEngine;

namespace Features.Player.Insrastructure.Interfaces
{
    public interface IReadOnlyMovementState
    {
        bool IsGrounded();
        Vector3 GetVelocity();
        Vector3 GetAcceleration();
        bool IsSliding();
    }
}