using Cysharp.Threading.Tasks;
using Features.Shared.Entities;
using Features.Shared.UseCases;

namespace Features.MainMenu.UseCases
{
    public sealed class StartGameUseCase
    {
        private readonly ISceneTransition _sceneTransition;

        public StartGameUseCase(ISceneTransition sceneTransition)
        {
            _sceneTransition = sceneTransition;
        }

        public void StartTutorial() => _sceneTransition.SwitchAsync(SceneId.MainMenu, SceneId.Tutorial).Forget();
        public void StartGameplay() => _sceneTransition.SwitchAsync(SceneId.MainMenu, SceneId.Gameplay).Forget();
    }
}
