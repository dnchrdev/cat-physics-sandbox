using Features.Player.Entities;
using Features.Player.UseCases.Ports;

namespace Features.Player.UseCases.Movement
{
    public sealed class WalkMode : IMovementMode
    {
        private const float SlideStartSpeed = 0f;
        private const float SlideSteerAcceleration = 0f;

        private readonly MovementState _state;
        private readonly ICharacterMotor _motor;
        private readonly ISurfaceProvider _surfaces;
        private readonly MovementRules _rules;

        public WalkMode(MovementState state, ICharacterMotor motor, ISurfaceProvider surfaces, MovementRules rules)
        {
            _state = state;
            _motor = motor;
            _surfaces = surfaces;
            _rules = rules;
        }

        public MovementMode Enter() => _state.RequestedJump ? MovementMode.Jump : MovementMode.Walk;

        public MovementMode FixedTick(float deltaTime)
        {
            var ground = _motor.ProbeGround( true);
            _state.Ground = ground;

            _state.Current.Grounded = ground.IsGrounded;
            _state.Current.Stable = ground.IsStable;
            _state.Current.Velocity = _motor.GetVelocity();
            _state.Current.Acceleration = (_state.Current.Velocity - _state.Previous.Velocity) / deltaTime;

            _state.TimeSinceUngrounded = 0f;
            _state.UngroundedDueToJump = false;
            _state.Surface = _surfaces.GetSurfaceUnderCharacter();

            if (!ground.IsGrounded) return MovementMode.Airborne;
            if (_state.RequestedJump) return MovementMode.Jump;

            if (ground.IsStable)
                Walk(ground, deltaTime);
            else
                Slide(ground, deltaTime);

            return MovementMode.Walk;
        }

        private void Walk(GroundProbe ground, float deltaTime)
        {
            var (velocity, acceleration) = LocomotionPhysics.Walk(
                _motor.GetVelocity(),
                ground.AdjustmentVelocity,
                ground.Normal,
                wasOnStableGround: _state.Previous.Grounded && _state.Previous.Stable,
                _state.RequestedMovement,
                _state.Surface,
                deltaTime);

            _state.Current.Acceleration = acceleration;
            _motor.SetVelocity(velocity);
        }

        private void Slide(GroundProbe ground, float deltaTime)
        {
            var rawVelocity = _motor.GetVelocity();
            var justStartedSliding = !_state.Previous.Grounded || _state.Previous.Stable;

            if (justStartedSliding)
            {
                _motor.SetVelocity(LocomotionPhysics.SlideStart(rawVelocity, ground.Normal, SlideStartSpeed));
                return;
            }

            _motor.SetVelocity(LocomotionPhysics.SlideContinue(
                _rules.SlideFriction,
                _rules.SlideGravity,
                SlideSteerAcceleration,
                _state.RequestedMovement.normalized,
                ground.AdjustmentVelocity,
                rawVelocity,
                ground.Normal,
                deltaTime));
        }
    }
}
