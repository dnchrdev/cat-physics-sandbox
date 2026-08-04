using Cysharp.Threading.Tasks;
using Feature.Core;
using Feature.Scene;
using Feature.Storage;
using Feature.UI;

namespace Feature.MainMenu
{
    public class StartGameUseCase
    {
        private IStorageDataService _storageDataService;
        private SceneLoaderService _levelLoaderService;

        public StartGameUseCase(IStorageDataService storageDataService, SceneLoaderService levelLoaderService)
        {
            _storageDataService = storageDataService;
            _levelLoaderService = levelLoaderService;
        }

        public void StartGame(bool isTutorial)
        {
            _storageDataService.Save();
            _levelLoaderService.GoToNextScene(isTutorial? "Tutorial": "Gameplay", "MainMenu").Forget();
        }

    }
}
