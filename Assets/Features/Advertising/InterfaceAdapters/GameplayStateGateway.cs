using Features.Advertising.UseCases.Ports;
using Features.Core.UseCases;
using Features.Shared.Entities.Ports;

namespace Features.Advertising.InterfaceAdapters
{
    public sealed class GameplayStateGateway : IGameplayStateGateway
    {
        private readonly GamePauseService _pause;
        private readonly IReadOnlyPlayer _player;

        public GameplayStateGateway(GamePauseService pause, IReadOnlyPlayer player)
        {
            _pause = pause;
            _player = player;
        }

        public bool IsGameplayActive => !_pause.IsPaused && _player.IsAlive;
    }
}
