using System.Collections;
using UnityEngine;
using Zenject;

namespace Feature.Advertising
{
    public class AdvInstaller : MonoInstaller
    {
        [SerializeField] private AdvView _view;
        
        public override void InstallBindings()
        {
            Container.Bind<IAdvRequestService>().To<AdvRequestService>().AsSingle();
            Container.BindInterfacesTo<InterstitialAdvService>().AsSingle().NonLazy();
            Container.BindInterfacesTo<AdvView>().FromInstance(_view).AsSingle();
        }
    }
}