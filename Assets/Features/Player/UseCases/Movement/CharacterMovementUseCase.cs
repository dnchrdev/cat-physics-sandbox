using System;
using Features.Player.Entities;
using Features.Player.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Player.UseCases.Movement
{
    public sealed class CharacterMovementUseCase
    {
        private readonly MovementState _state;
        private readonly IReadOnlyPlayer _player;
        private readonly ICharacterMotor _motor;
        private readonly IReadOnlyCameraTransform _camera;
        private readonly WalkMode _walk;
        private readonly AirborneMode _airborne;
        private readonly JumpMode _jump;

        private MovementMode _mode = MovementMode.Walk;

        private bool IsDead => _mode == MovementMode.Dead;

        public CharacterMovementUseCase(
            MovementState state,
            IReadOnlyPlayer player,
            ICharacterMotor motor,
            IReadOnlyCameraTransform camera,
            ISurfaceProvider surfaces,
            WalkMode walk,
            AirborneMode airborne,
            JumpMode jump)
        {
            _state = state;
            _player = player;
            _motor = motor;
            _camera = camera;
            _walk = walk;
            _airborne = airborne;
            _jump = jump;

            _state.Surface = surfaces.DefaultSurface;
        }

        public void Move(Vector2 input)
        {
            if (IsDead) return;
            _state.RequestMove(_camera.Rotation, input);
        }

        public void PressJump()
        {
            if (IsDead) return;
            _state.PressJump();
        }

        public void ReleaseJump()
        {
            if (IsDead) return;
            _state.ReleaseJump();
        }

        public void Tick(float deltaTime)
        {
            if (IsDead)
            {
                TickDead();
                return;
            }

            if (!_player.IsAlive)
            {
                SwitchTo(MovementMode.Dead);
                return;
            }

            _state.UpdateJumpBuffer();
            _state.TimeSinceJumped += deltaTime;
        }

        public void FixedTick(float deltaTime)
        {
            if (IsDead) return;

            _state.Previous = _state.Current;
            SwitchTo(ModeOf(_mode).FixedTick(deltaTime));
        }

        private void TickDead()
        {
            _state.Current.Acceleration = Vector3.zero;
            _motor.SetVelocity(Vector3.zero);

            if (!_player.IsAlive) return;
            SwitchTo(_state.Ground.IsGrounded ? MovementMode.Walk : MovementMode.Airborne);
        }

        private void SwitchTo(MovementMode next)
        {
            while (next != _mode)
            {
                _mode = next;
                next = next == MovementMode.Dead ? EnterDead() : ModeOf(next).Enter();
            }
        }

        private MovementMode EnterDead()
        {
            _state.ClearRequests();
            return MovementMode.Dead;
        }

        private IMovementMode ModeOf(MovementMode mode) => mode switch
        {
            MovementMode.Walk => _walk,
            MovementMode.Airborne => _airborne,
            MovementMode.Jump => _jump,
            _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, "Mode has no movement logic")
        };
    }
}
