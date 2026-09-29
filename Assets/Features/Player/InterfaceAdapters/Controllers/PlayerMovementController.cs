using System;
using Features.Player.UseCases;
using Features.Player.UseCases.Movement;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Player.InterfaceAdapters.Controllers
{
    public sealed class PlayerMovementController : IInitializable, IDisposable, ITickable, IFixedTickable
    {
        private readonly CharacterMovementUseCase _movement;
        private readonly SyncPlayerPositionUseCase _syncPosition;
        private readonly IMovementInput _input;

        public PlayerMovementController(
            CharacterMovementUseCase movement,
            SyncPlayerPositionUseCase syncPosition,
            IMovementInput input)
        {
            _movement = movement;
            _syncPosition = syncPosition;
            _input = input;
        }

        public void Initialize()
        {
            _input.HorizontalMoveEvent += _movement.Move;
            _input.JumpStartEvent += _movement.PressJump;
            _input.JumpReleaseEvent += _movement.ReleaseJump;
        }

        public void Dispose()
        {
            _input.HorizontalMoveEvent -= _movement.Move;
            _input.JumpStartEvent -= _movement.PressJump;
            _input.JumpReleaseEvent -= _movement.ReleaseJump;
        }

        public void Tick()
        {
            _movement.Tick(Time.deltaTime);
            _syncPosition.Execute();
        }

        public void FixedTick()
        {
            _movement.FixedTick(Time.fixedDeltaTime);
        }
    }
}
