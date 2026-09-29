using UnityEngine;

namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyMovementState
    {
        bool IsGrounded();
        Vector3 GetVelocity();
        Vector3 GetAcceleration();
        bool IsSliding();
    }
}
