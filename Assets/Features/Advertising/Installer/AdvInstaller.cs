using Features.Advertising.Application;
using Features.Advertising.Infrastructure;
using Features.Advertising.Infrastructure.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Advertising.Installer
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