using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Configs;
using Features.UI.Presentation;
using Features.UI.Presentation.Interfaces;
using Features.UI.Utils;
using UnityEngine;
using Zenject;

namespace Features.UI.Installer
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreenView loadingScreenView;
        [SerializeField] private PanelsConfig _gameplayPanelConfig;

        public override void InstallBindings()
        {
            Container.Bind<UIAnimator>().AsSingle();
            Container.Bind<ILoadingScreenView>().FromInstance(loadingScreenView).AsSingle();
            Container.BindInterfacesTo<LoadingScreenService>().AsSingle();
            Container.Bind<CursorManager>().AsSingle();
            Container.Bind<PanelsConfig>().FromInstance(_gameplayPanelConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<UIPanelsManager>().AsSingle();
        }
    }
}