using Cysharp.Threading.Tasks;
using Feature.UI;
using System;

namespace Feature.Scene
{
    public class SceneLoaderService
    {
        private readonly ILogger _logger;
        private readonly SceneManager _sceneManager;
        private readonly ScenesConfig _scenesConfig;
        private readonly ILoadingScreenService _loadingScreenService;

        public SceneLoaderService(ILogger logger, SceneManager sceneManager, ScenesConfig scenesConfig, ILoadingScreenService loadingScreenService)
        {
            _logger = logger;
            _sceneManager = sceneManager;
            _scenesConfig = scenesConfig;
            _loadingScreenService = loadingScreenService;
        }

        public async UniTask GoToNextScene(string loadScenePath, bool loadingScreen = true)
        {
            if(loadingScreen)
                await _loadingScreenService.StartLoadingAsync();

            await LoadSceneAsync(loadScenePath);

            if (loadingScreen)
                await _loadingScreenService.EndLoadingAsync();
        }

        public async UniTask GoToNextScene(string loadScenePath, string unloadScenePath, bool loadingScreen = true)
        {
            if (loadingScreen)
                await _loadingScreenService.StartLoadingAsync();

            await UnloadSceneAsync(unloadScenePath);
            await LoadSceneAsync(loadScenePath);

            if (loadingScreen)
                await _loadingScreenService.EndLoadingAsync();
        }

        private async UniTask LoadSceneAsync(string loadScenePath)
        {
            if (string.IsNullOrWhiteSpace(loadScenePath))
            {
                _logger.LogError(this.GetType(), "Scene path is empty");
                return;
            }

            var loadResult = await _sceneManager.LoadAsync(_scenesConfig.GetScene(loadScenePath));

            if (!loadResult.IsSuccess)
            {
                _logger.LogError(this.GetType(), loadResult.Message);
                return;
            }
        }

        private async UniTask UnloadSceneAsync(string unloadScenePath)
        {
            if (string.IsNullOrWhiteSpace(unloadScenePath))
            {
                _logger.LogError(this.GetType(), "Scene path is empty");
                return;
            }

            var unloadResult = await _sceneManager.UnloadAsync(_scenesConfig.GetScene(unloadScenePath));

            if (!unloadResult.IsSuccess)
            {
                _logger.LogError(this.GetType(), unloadResult.Message);
                return;
            }

            return;
        }
    }
}