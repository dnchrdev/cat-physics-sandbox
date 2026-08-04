using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.UI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField] private LoadingScreen _loadingScreen;
        [SerializeField] private UIPanelConfig _gameplayPanelConfig;

        public override void InstallBindings()
        {
            Container.Bind<UIAnimator>().AsSingle();
            Container.Bind<ILoadingScreen>().FromInstance(_loadingScreen).AsSingle();
            Container.BindInterfacesTo<LoadingScreenService>().AsSingle();
            Container.Bind<CursorManager>().AsSingle();
            Container.Bind<UIPanelConfig>().FromInstance(_gameplayPanelConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<UIPanelsManager>().AsSingle();
        }
    }
}