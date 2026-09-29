using Features.MainMenu.Infrastructure;
using Features.MainMenu.InterfaceAdapters.Controllers;
using Features.MainMenu.InterfaceAdapters.Presenters;
using Features.MainMenu.InterfaceAdapters.Ports;
using Features.MainMenu.UseCases;
using UnityEngine;
using Zenject;

namespace Features.MainMenu.Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuView _view;

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<IMainMenuView>().FromInstance(_view).AsSingle();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<MainMenuController>().AsSingle();
            Container.BindInterfacesTo<MainMenuPresenter>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<OpenMainMenuUseCase>().AsSingle();
            Container.Bind<StartGameUseCase>().AsSingle();
            Container.Bind<ChooseControlUseCase>().AsSingle();
        }
    }
}
