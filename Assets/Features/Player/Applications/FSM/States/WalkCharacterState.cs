using UnityEngine;

namespace Feature.PlayerFeature
{
    public class WalkCharacterState : GroudedCharacterState
    {
        public WalkCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void FixedTick(float dt)
        {
            base.FixedTick(dt);
            if (Stop) return;

            if (Motor.IsStable())
            {
                if (State.TimeSinceJumped > 0.15f)
                    Motor.SetExtendSensorRange(true);

                var rawVelocity = Motor.GetVelocity();

                var groundAdj = Motor.GetGroundAdjustmentVelocity();

                var speed = SurfaceDetector.CurrentSurface.WalkSpeed;

                var currentPlanarVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);

                if ((!State.LastState.Grounded || !State.LastState.Stable))
                {
                    currentPlanarVelocity = Vector3.ProjectOnPlane(rawVelocity, Motor.GetGroundNormal());
                }

                if (currentPlanarVelocity.sqrMagnitude < 0.01f && State.RequestedMovement.sqrMagnitude < 0.01f)
                    currentPlanarVelocity = Vector3.zero;

                var desiredVelocity = State.RequestedMovement * speed;
                var accelerationDir = desiredVelocity - currentPlanarVelocity;

                float responseValue = (State.RequestedMovement.sqrMagnitude <= 0.01f)
                    ? -1f
                    : Mathf.Clamp(Vector3.Dot(accelerationDir.normalized, currentPlanarVelocity), -1f, 1f);

                float acceleration = Mathf.Lerp(
                    SurfaceDetector.CurrentSurface.WalkMaxAccelerationResponse,
                    SurfaceDetector.CurrentSurface.WalkMinAccelerationResponse,
                    (responseValue + 1f) * 0.5f
                );

                Vector3 movementForce = accelerationDir.normalized * acceleration * dt;
                Vector3 finalHorizontal = currentPlanarVelocity + movementForce;

                float speedCap = Mathf.Max(currentPlanarVelocity.magnitude, speed);

                if (finalHorizontal.magnitude > speedCap)
                    finalHorizontal = finalHorizontal.normalized * speedCap;

                Vector3 finalVelocity = new Vector3(finalHorizontal.x, groundAdj.y, finalHorizontal.z);

                State.State.Acceleration = desiredVelocity - currentPlanarVelocity;

                Motor.SetVelocity(finalVelocity);
            }
            else
            {
                var wasInAir = !State.LastState.Grounded;
                var wasStable = State.LastState.Stable;
                Vector3 groundAdj = Motor.GetGroundAdjustmentVelocity();
                var rawVelocity = Motor.GetVelocity();
                Vector3 groundNormal = Motor.GetGroundNormal();

                if (wasInAir || wasStable)
                {
                    float effectiveSlideStartSpeed = 0f;

                    Vector3 impulse =
                        UnstableSlideCalculator.UnstableSlideImpulse(rawVelocity, groundNormal,
                            effectiveSlideStartSpeed);

                    Motor.SetVelocity(impulse);
                    return;
                }

                Motor.SetExtendSensorRange(true);

                Vector3 slide = UnstableSlideCalculator.UnstableSlideContinue(Config.SlideFriction, Config.SlideGravity,
                    0f, State.RequestedMovement.normalized, groundAdj, rawVelocity, groundNormal, dt);

                Motor.SetVelocity(slide);
            }
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);
        }
    }
}