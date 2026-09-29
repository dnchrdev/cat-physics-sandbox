using Features.Player.Entities;
using Features.Player.UseCases.Ports;
using UnityEngine;

namespace Features.Player.UseCases.Movement
{
    public sealed class JumpMode : IMovementMode
    {
        private const float SlideStartSpeed = 0f;

        private readonly MovementState _state;
        private readonly ICharacterMotor _motor;
        private readonly MovementRules _rules;

        private Vector3 _startVelocity;

        public JumpMode(MovementState state, ICharacterMotor motor, MovementRules rules)
        {
            _state = state;
            _motor = motor;
            _rules = rules;
        }

        public MovementMode Enter()
        {
            _state.ConsumeJumpRequest();
            _state.UngroundedDueToJump = true;
            _state.TimeSinceJumped = 0f;

            _startVelocity = LocomotionPhysics.JumpStart(_motor.GetVelocity());

            var ground = _state.Ground;
            if (ground.IsGrounded && !ground.IsStable)
            {
                _startVelocity = LocomotionPhysics.SlideStart(_startVelocity, ground.Normal, SlideStartSpeed);
                _motor.SetVelocity(_startVelocity);
                return MovementMode.Jump;
            }

            _motor.SetVelocity(Vector3.zero);
            return MovementMode.Jump;
        }

        public MovementMode FixedTick(float deltaTime)
        {
            var ground = _motor.ProbeGround( false);
            _state.Ground = ground;

            _state.Current.Grounded = _state.Previous.Grounded;
            _state.Current.Stable = _state.Previous.Stable;
            _state.Current.Velocity = _motor.GetVelocity();
            _state.Current.Acceleration = Vector3.zero;

            _motor.SetVelocity(LocomotionPhysics.Jump(
                _startVelocity,
                ground.AdjustmentVelocity,
                _state.Current.Stable,
                _rules.JumpSpeed));

            return MovementMode.Airborne;
        }
    }
}
