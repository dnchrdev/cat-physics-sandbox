using Features.MainMenu.Adapter;
using Features.MainMenu.Adapter.Interfaces;
using Features.MainMenu.Application;
using Features.MainMenu.Infrastructure;
using Features.Scene.Config;
using UnityEngine;
using Zenject;

namespace Features.MainMenu.Installer
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