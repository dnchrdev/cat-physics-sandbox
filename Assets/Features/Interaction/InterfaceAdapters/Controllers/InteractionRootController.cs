using System;
using Features.Interaction.UseCases;
using Features.Shared.Entities.Ports;
using UnityEngine;
using Zenject;

namespace Features.Interaction.InterfaceAdapters.Controllers
{
    public sealed class InteractionRootController : IInitializable, IDisposable, IFixedTickable
    {
        [Inject]  private readonly InteractionRootFollowUseCase _follow;
        [Inject]  private readonly IPlayerLifeEvents _playerLifeEvents;
        
        public void Initialize()
        {
            _follow.TeleportToPlayer();
            _playerLifeEvents.Respawned += _follow.TeleportToPlayer;
        }

        public void Dispose()
        {
            _playerLifeEvents.Respawned -= _follow.TeleportToPlayer;
        }

        public void FixedTick()
        {
            _follow.FixedTick(Time.fixedDeltaTime);
        }
    }
}
