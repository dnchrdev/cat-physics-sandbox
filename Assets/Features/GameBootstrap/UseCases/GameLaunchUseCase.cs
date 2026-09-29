using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.GameBootstrap.UseCases
{
    public sealed class GameLaunchUseCase
    {
        private readonly IStorageDataService _storage;
        private readonly ILoadingScreenService _loadingScreen;
        private readonly ISceneTransition _sceneTransition;

        public GameLaunchUseCase(
            IStorageDataService storage,
            ILoadingScreenService loadingScreen,
            ISceneTransition sceneTransition)
        {
            _storage = storage;
            _loadingScreen = loadingScreen;
            _sceneTransition = sceneTransition;
        }

        public async UniTask LaunchAsync()
        {
            await _loadingScreen.FadeInAsync();
            await _storage.Load();
            _sceneTransition.OpenCoveredAsync(SceneId.MainMenu).Forget();
        }
    }
}
