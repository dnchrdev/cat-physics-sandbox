using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.UI
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