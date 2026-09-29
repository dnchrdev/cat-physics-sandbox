using System;
using Features.Interaction.UseCases;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Interaction.InterfaceAdapters.Controllers
{
    public sealed class InteractionController : IInitializable, IDisposable, ITickable
    {
        [Inject] private readonly InteractionUseCase _interaction;
        [Inject] private readonly IInteractionInput _input;
        [Inject]  private readonly IReadOnlyPlayer _player;
        [Inject]  private readonly IPlayerLifeEvents _playerLifeEvents;

        public void Initialize()
        {
            _playerLifeEvents.Knockedout += _interaction.HandleKnockout;
        }

        public void Dispose()
        {
            _playerLifeEvents.Knockedout -= _interaction.HandleKnockout;
        }

        public void Tick()
        {
            if (!_player.IsAlive) return;
            _interaction.Execute(ReadRequest(), _player, Time.deltaTime);
        }

        private InteractionRequest ReadRequest() =>
            new InteractionRequest(_input.IsGrab, _input.IsHit, _input.IsThrow, _input.IsRelease);
    }
}
