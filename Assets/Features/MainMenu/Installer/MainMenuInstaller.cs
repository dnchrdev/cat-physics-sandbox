using Feature.Scene;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.MainMenu
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuPresenter _presenter;
        [SerializeField] private ScenesConfig _scenesConfig;

        public override void InstallBindings()
        {
            Container.Bind<StartGameUseCase>().AsSingle();
            Container.Bind<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<MainMenuPresenter>().FromInstance(_presenter).AsSingle();
            //Container.BindInterfacesAndSelfTo<PresenterOrchestrator>().AsSingle().NonLazy();
        }
    }
}