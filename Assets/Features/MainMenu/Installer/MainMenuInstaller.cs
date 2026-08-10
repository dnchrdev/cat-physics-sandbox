using Feature.Scene;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _view;
        [SerializeField] private ScenesConfig _scenesConfig;

        public override void InstallBindings()
        {
            Container.Bind<StartGameUseCase>().AsSingle();
            Container.Bind<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().AsSingle();
            Container.Bind<IMainMenuView>().FromInstance(_view);
        }
    }
}