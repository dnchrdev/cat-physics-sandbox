using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Storage.Entities.Ports;

namespace Features.Tutorial.UseCases
{
    public sealed class StartGameplayUseCase
    {
        private readonly IPlayerProgress _playerProgress;
        private readonly IStorageDataService _storage;
        private readonly IUIPanelsManager _panels;
        private readonly ISceneTransition _sceneTransition;

        public StartGameplayUseCase(
            IPlayerProgress playerProgress,
            IStorageDataService storage,
            IUIPanelsManager panels,
            ISceneTransition sceneTransition)
        {
            _playerProgress = playerProgress;
            _storage = storage;
            _panels = panels;
            _sceneTransition = sceneTransition;
        }

        public void Execute()
        {
            _playerProgress.SetTutorialCompleted(true);
            _storage.Save();
            _panels.OpenPanel(PanelMode.Gameplay);
            _panels.ClearAllPanels();
            _sceneTransition.SwitchAsync(SceneId.Tutorial, SceneId.Gameplay).Forget();
        }
    }
}
