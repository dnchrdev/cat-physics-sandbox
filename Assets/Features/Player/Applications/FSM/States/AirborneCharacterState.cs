using UnityEngine;

namespace Feature.PlayerFeature
{
    public class AirborneCharacterState : AliveCharacterState
    {
        public AirborneCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Motor.SetExtendSensorRange(false);
            Motor.CheckForGround();

            var planarRotation = Vector3.ProjectOnPlane
            (
                CameraForward,
                Vector3.up
            ).normalized;

            State.TimeSinceUngrounded = 0f;
        }

        public override void FixedTick(float dt)
        {
            base.FixedTick(dt);

            Motor.SetExtendSensorRange(false);
            Motor.CheckForGround();

            if (State.TimeSinceUngrounded < 0.1f && Motor.GetVelocity().y > 0f)
                Motor.SetForcedGrounded(false);

            State.State.Grounded = Motor.IsGrounded();
            State.State.Stable = Motor.IsStable();
            State.State.Velocity = Motor.GetVelocity();
            State.State.Acceleration = Vector3.zero;

            var hitCollider = Motor.GetGroundCollider();
            SurfaceDetector.GetSurfaceData(hitCollider == null ? null : hitCollider.gameObject);

            if (State.RequestedJump)
            {
                var canCoyoteJump = State.TimeSinceUngrounded < Config.CoyoteTime && !State.UngroundedDueToJump;

                if (canCoyoteJump)
                {
                    StateSwitcher.Switch<JumpCharacterState>();
                    return;
                }
                else
                {
                    State.TimeSinceJumpRequest += dt;
                    var canJumpLater = State.TimeSinceJumpRequest < Config.CoyoteTime;
                    State.RequestedJump = canJumpLater;
                }
            }

            if (Motor.IsGrounded())
            {
                StateSwitcher.Switch<WalkCharacterState>();
                return;
            }

            var planarRotation = Vector3.ProjectOnPlane
            (
                CameraForward,
                Vector3.up
            ).normalized;

            State.TimeSinceUngrounded += dt;

            var rawVelocity = Motor.GetVelocity();

            var currentPlanarVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);

            Vector3 planarVelocity = Vector3.ProjectOnPlane(rawVelocity, Vector3.up);
            float verticalVelocity = Vector3.Dot(rawVelocity, Vector3.up);

            if (State.RequestedMovement.sqrMagnitude > 0f)
            {
                var movementForce = State.RequestedMovement * (SurfaceDetector.CurrentSurface.AirAcceleration * dt);

                if (planarVelocity.magnitude < SurfaceDetector.CurrentSurface.AirSpeed)
                {
                    var targetPlanarVelocity = planarVelocity + movementForce;
                    targetPlanarVelocity = Vector3.ClampMagnitude(
                        targetPlanarVelocity,
                        SurfaceDetector.CurrentSurface.AirSpeed
                    );
                    movementForce = targetPlanarVelocity - planarVelocity;
                }
                else if (Vector3.Dot(planarVelocity, movementForce) > 0f)
                {
                    movementForce = Vector3.ProjectOnPlane(
                        movementForce,
                        planarVelocity.normalized
                    );
                }

                planarVelocity += movementForce;
            }

            verticalVelocity -= Config.Gravity * dt;

            Vector3 finalVelocity = new Vector3(
                planarVelocity.x,
                verticalVelocity, //+adjVelocity.y,
                planarVelocity.z
            );

            Motor.SetVelocity(finalVelocity);
        }
    }
}