using UnityEngine;

namespace Feature.PlayerFeature
{
    public class GroudedCharacterState : AliveCharacterState
    {
        public GroudedCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();

            if (State.RequestedJump)
            {
                StateSwitcher.Switch<JumpCharacterState>();
                Stop = true;
            }
        }

        public override void FixedTick(float dt)
        {
            base.FixedTick(dt);

            Motor.SetExtendSensorRange(true);
            Motor.CheckForGround();
            State.State.Grounded = Motor.IsGrounded();
            State.State.Stable = Motor.IsStable();
            State.State.Velocity = Motor.GetVelocity();
            State.State.Acceleration = (State.State.Velocity - State.LastState.Velocity) / dt;

            State.TimeSinceUngrounded = 0f;
            State.UngroundedDueToJump = false;

            var hitCollider = Motor.GetGroundCollider();
            SurfaceDetector.GetSurfaceData(hitCollider == null ? null : hitCollider.gameObject);

            if (State.State.Grounded == false)
            {
                StateSwitcher.Switch<AirborneCharacterState>();
                Stop = true;
                return;
            }

            var planarRotation = Vector3.ProjectOnPlane
            (
                CameraForward,
                Vector3.up
            ).normalized;

            if (State.RequestedJump)
            {
                StateSwitcher.Switch<JumpCharacterState>();
                Stop = true;
                return;
            }
        }
    }
}