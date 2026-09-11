using Cysharp.Threading.Tasks;
using Features.Core.Utils;
using Features.Scene.Data;
using Features.Scene.Infrastructure;
using Features.Storage.Domain;
using Features.Storage.Infrastructure;
using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Data;
using Features.UI.Presentation.Interfaces;
using Zenject;

namespace Features.Tutorial.Application
{
    public class CompleteTutorialUseCase
    {
        [Inject] private readonly SceneLoadingService _sceneLoadingService;
        [Inject] private readonly PlayerProgress _playerProgress;
        [Inject] private readonly UIPanelsManager _panelsManager;
        [Inject] private readonly StorageDataService _storageDataService;
        [Inject] private readonly ILoadingScreenService _loadingScreenService;
        
        public async UniTask<Result> StartGameplayAsync()
        {
            _playerProgress.SetTutorialCompleted(true);
            _storageDataService.Save();
            _panelsManager.OpenPanel(PanelMode.Gameplay);
            _panelsManager.ClearAllPanels();
            
            await _loadingScreenService.FadeInAsync();
            
            var unloadResult = await _sceneLoadingService.UnloadAsync(SceneId.Tutorial);
            if (unloadResult.IsSuccess == false) return unloadResult;

            var loadResult = await _sceneLoadingService.LoadAsync(SceneId.Gameplay, false);
            if (loadResult.IsSuccess == false) return loadResult;

            await UniTask.WaitForEndOfFrame();
            
            var activateResult = await _sceneLoadingService.ActivateAsync(SceneId.Gameplay);
            if (activateResult.IsSuccess == false) return activateResult;

            await _loadingScreenService.FadeOutAsync();

            return Result.Success();
        }
    }
}