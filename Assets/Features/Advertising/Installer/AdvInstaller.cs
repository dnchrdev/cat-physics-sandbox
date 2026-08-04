using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.Advertising
{
    public class AdvInstaller : MonoInstaller
    {
        [SerializeField] private AdvPresenter _advPresenter;

        public override void InstallBindings()
        {
            Container.Bind<IAdvRequestService>().To<AdvRequestService>().AsSingle();
            Container.BindInterfacesTo<InterstitialAdvEverySeconds>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<AdvPresenter>().FromInstance(_advPresenter).AsSingle();
        }
    }
}