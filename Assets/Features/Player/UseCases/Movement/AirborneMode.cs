using Features.Player.Entities;
using Features.Player.UseCases.Ports;
using UnityEngine;

namespace Features.Player.UseCases.Movement
{
    public sealed class AirborneMode : IMovementMode
    {
        private const float JustLeftGroundTime = 0.1f;

        private readonly MovementState _state;
        private readonly ICharacterMotor _motor;
        private readonly ISurfaceProvider _surfaces;
        private readonly MovementRules _rules;

        public AirborneMode(MovementState state, ICharacterMotor motor, ISurfaceProvider surfaces, MovementRules rules)
        {
            _state = state;
            _motor = motor;
            _surfaces = surfaces;
            _rules = rules;
        }

        public MovementMode Enter()
        {
            _state.Ground = _motor.ProbeGround(false);
            _state.TimeSinceUngrounded = 0f;
            return MovementMode.Airborne;
        }

        public MovementMode FixedTick(float deltaTime)
        {
            var ground = _motor.ProbeGround( false);
            
            if (_state.TimeSinceUngrounded < JustLeftGroundTime && _motor.GetVelocity().y > 0f)
                ground = ground.AsUngrounded();

            _state.Ground = ground;
            _state.Current.Grounded = ground.IsGrounded;
            _state.Current.Stable = ground.IsStable;
            _state.Current.Velocity = _motor.GetVelocity();
            _state.Current.Acceleration = Vector3.zero;
            _state.Surface = _surfaces.GetSurfaceUnderCharacter();

            if (_state.RequestedJump)
            {
                var canCoyoteJump = _state.TimeSinceUngrounded < _rules.CoyoteTime && !_state.UngroundedDueToJump;
                if (canCoyoteJump) return MovementMode.Jump;

                _state.AgeJumpRequest(deltaTime, _rules.CoyoteTime);
            }

            if (ground.IsGrounded) return MovementMode.Walk;

            _state.TimeSinceUngrounded += deltaTime;

            _motor.SetVelocity(LocomotionPhysics.Air(
                _motor.GetVelocity(),
                _state.RequestedMovement,
                _state.Surface,
                _rules.Gravity,
                deltaTime));

            return MovementMode.Airborne;
        }
    }
}
