using Features.Advertising.Entities;
using Features.Advertising.Infrastructure;
using Features.Advertising.InterfaceAdapters;
using Features.Advertising.InterfaceAdapters.Controllers;
using Features.Advertising.InterfaceAdapters.Presenters;
using Features.Advertising.UseCases;
using UnityEngine;
using Zenject;

namespace Features.Advertising.Installer
{
    public class AdvInstaller : MonoInstaller
    {
        private const float ADV_EVERY_SECONDS = 60f * 1f + 1f;
        private const float SHOW_COUNTDOWN_AT_SECONDS = 10f + 1f;

        [SerializeField] private AdvView _view;

        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
            InstallEntities();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<AdvView>().FromInstance(_view).AsSingle();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<YandexAdvService>().AsSingle();
            Container.BindInterfacesTo<GameplayStateGateway>().AsSingle();
            Container.BindInterfacesTo<InterstitialAdvPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesTo<InterstitialAdvController>().AsSingle().NonLazy();
        }

        private void InstallUseCases()
        {
            Container.Bind<InterstitialAdvUseCase>().AsSingle();
        }

        private void InstallEntities()
        {
            Container.Bind<InterstitialAdvTimer>()
                .AsSingle()
                .WithArguments(ADV_EVERY_SECONDS, SHOW_COUNTDOWN_AT_SECONDS);
        }
    }
}
