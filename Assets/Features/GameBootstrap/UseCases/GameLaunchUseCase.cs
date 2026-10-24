using System.Threading;
using Cysharp.Threading.Tasks;
using Features.GameBootstrap.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.GameBootstrap.UseCases
{
    public sealed class GameLaunchUseCase
    {
        private readonly IStorageDataService _storage;
        private readonly ILoadingScreenService _loadingScreen;
        private readonly ISceneTransition _sceneTransition;
        private readonly ICommonAssetsHolder _commonAssets;

        public GameLaunchUseCase(
            IStorageDataService storage,
            ILoadingScreenService loadingScreen,
            ISceneTransition sceneTransition,
            ICommonAssetsHolder commonAssets)
        {
            _storage = storage;
            _loadingScreen = loadingScreen;
            _sceneTransition = sceneTransition;
            _commonAssets = commonAssets;
        }

        public async UniTask LaunchAsync(CancellationToken  ct)
        {
            await _loadingScreen.FadeInAsync();
            await _commonAssets.LoadAsync(ct);
            await _storage.Load();
            _sceneTransition.OpenCoveredAsync(SceneId.MainMenu).Forget();
        }
    }
}
