using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface IReadOnlyMovementState
    {
        bool IsGrounded();
        Vector3 GetVelocity();
        Vector3 GetAcceleration();
        bool IsSliding();
    }
}