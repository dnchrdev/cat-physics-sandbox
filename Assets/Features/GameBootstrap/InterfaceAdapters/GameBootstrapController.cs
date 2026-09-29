using Cysharp.Threading.Tasks;
using Features.GameBootstrap.UseCases;
using Zenject;

namespace Features.GameBootstrap.InterfaceAdapters
{
    public sealed class GameBootstrapController : IInitializable
    {
        private readonly GameLaunchUseCase _gameLaunch;

        public GameBootstrapController(GameLaunchUseCase gameLaunch)
        {
            _gameLaunch = gameLaunch;
        }

        public void Initialize() => _gameLaunch.LaunchAsync().Forget();
    }
}
