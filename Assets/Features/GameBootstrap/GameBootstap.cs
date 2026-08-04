using Cysharp.Threading.Tasks;
using Feature.Scene;
using Feature.UI;
using System;
using UnityEngine;
using Zenject;

namespace Feature.GameBootstrap
{
    public class GameBootstap : MonoBehaviour, IInitializable
    {
        [SerializeField] private int _saveID;

        private SceneLoaderService _sceneLoaderService;
        private StorageDataService _storageDataService;
        private ILoadingScreenService _loadingScreenService;

        [Inject]
        private void Construct(SceneLoaderService sceneLoaderService, StorageDataService storageDataService, ILoadingScreenService loadingScreenService)
        {
            _sceneLoaderService = sceneLoaderService;
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
            await _loadingScreenService.StartLoadingAsync();    
            _storageDataService.Load(LoadMainMenu);  
        }

        private void LoadMainMenu()
        {
            HandleMainManuLoadAsync().Forget();
        }

        private async UniTask HandleMainManuLoadAsync()
        {
            await _sceneLoaderService.GoToNextScene("MainMenu", false);
            await _loadingScreenService.EndLoadingAsync();
        }
    }
}
