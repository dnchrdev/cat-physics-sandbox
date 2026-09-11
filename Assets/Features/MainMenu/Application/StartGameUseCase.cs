using Cysharp.Threading.Tasks;
using Features.Core.Utils;
using Features.Scene.Data;
using Features.Scene.Infrastructure;
using Features.Storage.Infrastructure;
using Features.UI.Presentation.Interfaces;
using Zenject;

namespace Features.MainMenu.Application
{
    public class StartGameUseCase
    {
        [Inject] private readonly SceneLoadingService _sceneLoadingService;
        [Inject] private readonly ILoadingScreenService _loadingScreenService;
        [Inject] private readonly StorageDataService _storageDataService;

        
        public async UniTask<Result> StartTutorialAsync()
        {
            return await LoadSceneUnloadMainMenuAsync(SceneId.Tutorial);
        }
        
        public async UniTask<Result> StartGameplayAsync()
        {
            return await LoadSceneUnloadMainMenuAsync(SceneId.Gameplay);
        }

        private async UniTask<Result> LoadSceneUnloadMainMenuAsync(SceneId scene)
        {
            await _loadingScreenService.FadeInAsync();

            var unloadResult = await _sceneLoadingService.UnloadAsync(SceneId.MainMenu);
            if (unloadResult.IsSuccess == false) return unloadResult;
 
            var loadResult = await _sceneLoadingService.LoadAsync(scene, false);
            if (loadResult.IsSuccess == false) return loadResult;

            await UniTask.WaitForEndOfFrame();
                
            var activateResult = await _sceneLoadingService.ActivateAsync(scene);
            if (activateResult.IsSuccess == false) return activateResult;
            
            await _loadingScreenService.FadeOutAsync();
            return Result.Success();
        }
    }
}
