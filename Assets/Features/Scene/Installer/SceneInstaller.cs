using Features.Scene.Infrastructure.Configs;
using Features.Scene.InterfaceAdapters;
using Features.Scene.UseCases;
using Features.Scene.UseCases.Ports;
using Features.Shared.UseCases;
using UnityEngine;
using Zenject;

namespace Features.Scene.Installer
{
    public sealed class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfig _scenesConfig;

        public override void InstallBindings()
        {
            InstallUseCases();
            InstallAdapters();
            InstallInfrastructure();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();
        }

        private void InstallAdapters()
        {
            Container.Bind<ISceneLoaderProvider>().To<AddressablesSceneLoaderProvider>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.Bind<ISceneLoadingService>().To<SceneLoadingService>().AsSingle();
            Container.Bind<ISceneTransition>().To<SceneTransitionUseCase>().AsSingle();
        }
    }
}
