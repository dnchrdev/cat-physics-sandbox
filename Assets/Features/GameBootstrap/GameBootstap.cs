using Cysharp.Threading.Tasks;
using Feature.Scene;
using Feature.UI;
using System;
using Feature.Core;
using UnityEngine;
using Zenject;

namespace Feature.GameBootstrap
{
    public class GameBootstap : MonoBehaviour, IInitializable
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
