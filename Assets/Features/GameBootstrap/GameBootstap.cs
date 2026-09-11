using Cysharp.Threading.Tasks;
using Features.Core.Utils;
using Features.Scene.Data;
using Features.Scene.Infrastructure;
using Features.Storage.Infrastructure;
using Features.UI.Presentation.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.GameBootstrap
{
    public class GameBootstrap : MonoBehaviour, IInitializable
    {
        [SerializeField] private int _saveID;

        private SceneLoadingService _sceneLoadingService;
        private StorageDataService _storageDataService;
        private ILoadingScreenService _loadingScreenService;

        [Inject]
        private void Construct(SceneLoadingService sceneLoadingService, StorageDataService storageDataService, ILoadingScreenService loadingScreenService)
        {
            _sceneLoadingService = sceneLoadingService;
            _storageDataService = storageDataService;
            _loadingScreenService = loadingScreenService;
        }

        public void Initialize()
        {
            _storageDataService.InitSaveID(_saveID);
            LoadGame().Forget();
        }

        private async UniTask LoadGame()
        {
            await _loadingScreenService.FadeInAsync();    
            _storageDataService.Load(OnSavedDataWasLoaded);  
        }

        private void OnSavedDataWasLoaded()
        {
            HandleMainMenuLoadAsync().Forget();
        }

        private async UniTask<Result> HandleMainMenuLoadAsync()
        {
            var loadResult = await _sceneLoadingService.LoadAsync(SceneId.MainMenu, false);
            if (loadResult.IsSuccess == false) return loadResult;
           
            await UniTask.WaitForEndOfFrame();
            
            var activateResult = await _sceneLoadingService.ActivateAsync(SceneId.MainMenu);
            if (activateResult.IsSuccess == false) return activateResult;
            
            await _loadingScreenService.FadeOutAsync();

            return Result.Success();
        }
    }
}
