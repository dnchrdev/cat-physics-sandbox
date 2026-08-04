using UnityEngine;

namespace Feature.PlayerFeature
{
    public class DeadCharacterState : BaseCharacterState
    {
        public DeadCharacterState(MovementContext context, MovementState state) : base(context, state)
        {
        }

        public override void Enter()
        {
            base.Enter();
            State.RequestedMovement = Vector3.zero;
            State.RequestedJump = false;
            State.RequestedSustainJump = false;
            State.RequestedRotation = Quaternion.identity;
        }

        public override void Tick(float dt)
        {
            base.Tick(dt);
            State.State.Acceleration = Vector3.zero;
            Motor.SetVelocity(Vector3.zero);

            if (Player.IsAlive)
            {
                if (Motor.IsGrounded())
                {
                    StateSwitcher.Switch<WalkCharacterState>();
                    return;
                }
                else
                    StateSwitcher.Switch<AirborneCharacterState>();
            }
        }
    }
}