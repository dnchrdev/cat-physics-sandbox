using Cysharp.Threading.Tasks;
using Feature.Scene;
using Feature.Storage;
using Feature.UI;


namespace Feature.Tutorial
{
    public class CompleteTutorialUseCase
    {
        private readonly SceneLoaderService _sceneLoader;
        private PlayerProgress _playerProgress;
        private UIPanelsManager _panelsManager;
        private StorageDataService _storageDataService;

        public CompleteTutorialUseCase(SceneLoaderService sceneLoader, PlayerProgress playerProgress, UIPanelsManager panelsManager, StorageDataService storageDataService)
        {
            _sceneLoader = sceneLoader;
            _playerProgress = playerProgress;
            _panelsManager = panelsManager;
            _storageDataService = storageDataService;
        }

        public void StartGame()
        {
            _playerProgress.SetTutorialCompleted(true);
            _storageDataService.Save();
            _panelsManager.OpenPanel(UIPanelTag.Gameplay);
            _panelsManager.ClearAllPanels();
            _sceneLoader.GoToNextScene(loadScenePath: "Gameplay", unloadScenePath: "Tutorial").Forget();
        }

    }
}