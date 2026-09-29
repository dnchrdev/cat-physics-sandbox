using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Player.Entities
{
    public class MovementState : IReadOnlyMovementState
    {
        public Vector3 RequestedMovement { get; private set; }
        public bool RequestedJump { get; private set; }
        public bool RequestedSustainJump { get; private set; }

        public GroundProbe Ground = GroundProbe.None;
        public SurfaceProperties Surface;

        public CharacterKinematics Current;
        public CharacterKinematics Previous;

        public float TimeSinceJumped;
        public float TimeSinceUngrounded;
        public float TimeSinceJumpRequest;
        public bool UngroundedDueToJump;

        private bool _previousJump;

        public void RequestMove(Quaternion viewRotation, Vector2 input)
        {
            var movement = viewRotation * new Vector3(input.x, 0f, input.y);
            RequestedMovement = Vector3.ProjectOnPlane(movement, Vector3.up).normalized * movement.magnitude;
        }

        public void PressJump()
        {
            RequestedJump = true;
            RequestedSustainJump = true;
        }

        public void ReleaseJump()
        {
            RequestedJump = false;
            RequestedSustainJump = false;
        }

        public void ClearRequests()
        {
            RequestedMovement = Vector3.zero;
            RequestedJump = false;
            RequestedSustainJump = false;
        }
        
        public void UpdateJumpBuffer()
        {
            var currentJump = RequestedJump;
            RequestedJump = RequestedJump && !_previousJump;
            _previousJump = currentJump;

            var wasRequestedJump = RequestedJump;
            RequestedJump = RequestedJump || RequestedSustainJump;

            if (RequestedJump && !wasRequestedJump && currentJump)
                TimeSinceJumpRequest = 0f;
        }
        
        public void AgeJumpRequest(float deltaTime, float bufferTime)
        {
            TimeSinceJumpRequest += deltaTime;
            RequestedJump = TimeSinceJumpRequest < bufferTime;
        }

        public void ConsumeJumpRequest() => RequestedJump = false;

        public bool IsGrounded() => Current.Grounded;
        public Vector3 GetVelocity() => Current.Velocity;
        public Vector3 GetAcceleration() => Current.Acceleration;
        public bool IsSliding() => Current.Stable is false;
    }
}
