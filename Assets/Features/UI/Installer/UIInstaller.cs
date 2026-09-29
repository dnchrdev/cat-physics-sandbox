using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Views;
using Features.UI.Infrastructure.Config;
using Features.UI.InterfaceAdapters;
using Features.UI.InterfaceAdapters.Ports;
using Features.UI.UseCases;
using Features.UI.UseCases.Ports;
using UnityEngine;
using Zenject;

namespace Features.UI.Installer
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenView _loadingScreenView;
        [SerializeField] private PanelsConfig _panelsConfig;

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallInfrastructure()
        {
            Container.Bind<ILoadingScreenView>().FromInstance(_loadingScreenView).AsSingle();
            Container.Bind<IPanelsConfigProvider>().FromInstance(_panelsConfig).AsSingle();
            Container.Bind<UIAnimator>().AsSingle();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<UIController>().AsSingle();
            Container.BindInterfacesTo<LoadingScreenService>().AsSingle();
            Container.BindInterfacesTo<CursorManager>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.BindInterfacesAndSelfTo<UIPanelsManager>().AsSingle();
        }
    }
}
