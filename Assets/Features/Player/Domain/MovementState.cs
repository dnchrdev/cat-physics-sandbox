using UnityEngine;

namespace Feature.PlayerFeature
{
    public struct CharacterState
    {
        public bool Grounded;
        public bool Stable;
        public Vector3 Velocity;
        public Vector3 Acceleration;
    }

    public class MovementState : IReadOnlyMovementState
    {
        public Quaternion RequestedRotation;
        public Vector3 RequestedMovement;
        public bool RequestedJump;

        public CharacterState State;
        public CharacterState LastState;
        public ICharacterState CharacterState;

        public float TimeSinceJumped;
        public float TimeSinceUngrounded;
        public float TimeSinceJumpRequest;
        public bool UngroundedDueToJump;

        public bool RequestedSustainJump;

        public bool IsGrounded() => State.Grounded;
        public Vector3 GetVelocity() => State.Velocity;
        public Vector3 GetAcceleration() => State.Acceleration;
        public bool IsSliding() => State.Stable is false;
    }
}