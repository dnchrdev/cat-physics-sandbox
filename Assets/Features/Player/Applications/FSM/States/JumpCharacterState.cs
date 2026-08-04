using UnityEngine;

namespace Feature.PlayerFeature
{
    public class JumpCharacterState : AliveCharacterState
    {
        private Vector3 _jumpStartRawVelocity;

        public JumpCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();

            Motor.SetExtendSensorRange(false);
            //Motor.SetForcedGrounded(false);
            State.RequestedJump = false;
            State.UngroundedDueToJump = true;
            State.TimeSinceJumped = 0f;


            var rawVelocity = Motor.GetVelocity();

            if (rawVelocity.y > 0f)
                rawVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);

            _jumpStartRawVelocity = rawVelocity;

            var wasInAir = State.LastState.Grounded == false;

            var grounded = Motor.IsGrounded();

            if (Motor.IsStable() == false && grounded)
            {
                Vector3 groundNormal = Motor.GetGroundNormal();
                float effectiveSlideStartSpeed = 0f;

                Vector3 impulse =
                    UnstableSlideCalculator.UnstableSlideImpulse(rawVelocity, groundNormal, effectiveSlideStartSpeed);

                Motor.SetVelocity(impulse);
                _jumpStartRawVelocity = impulse;
                return;
            }

            Motor.SetVelocity(Vector3.zero);
        }


        public override void FixedTick(float dt)
        {
            base.FixedTick(dt);

            Motor.SetExtendSensorRange(false);
            Motor.SetForcedGrounded(false);
            Motor.CheckForGround();

            State.State.Grounded = State.LastState.Grounded;
            State.State.Stable = State.LastState.Stable;
            State.State.Velocity = Motor.GetVelocity();
            State.State.Acceleration = Vector3.zero;

            //rawVelocity = Motor.GetVelocity();
            var rawVelocity = _jumpStartRawVelocity;

            Vector3 jumpVelocity = Vector3.zero;

            var currentPlanarVelocity = new Vector3(rawVelocity.x, 0f, rawVelocity.z);
            Vector3 adj = Motor.GetGroundAdjustmentVelocity();
            Vector3 cleanVelocity = rawVelocity - adj;

            float cleanVerticalSpeed = Vector3.Dot(cleanVelocity, Vector3.up);
            float jumpVerticalSpeed = Mathf.Max(cleanVerticalSpeed, Config.JumpSpeed);

            Vector3 currentHorizontalVelocity = Vector3.ProjectOnPlane(cleanVelocity, Vector3.up);

            var notZeroAdj = (State.State.Stable == false) || adj.y > 0f;

            jumpVelocity = new Vector3(
                currentHorizontalVelocity.x,
                jumpVerticalSpeed,
                currentHorizontalVelocity.z
            ) + (notZeroAdj ? adj : Vector3.zero);

            Motor.SetVelocity(jumpVelocity);
            StateSwitcher.Switch<AirborneCharacterState>();
        }
    }
}