using Features.Scene.Config;
using Features.Scene.Infrastructure;
using UnityEngine;
using Zenject;

namespace Features.Scene.Installer
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private ScenesConfig _scenesConfig;
        public override void InstallBindings()
        {
            Container.Bind<SceneLoadingService>().AsSingle();
            Container.Bind<ISceneLoaderProvider>().To<AddressablesSceneLoaderProvider>().AsSingle();
            Container.Bind<ScenesConfig>().FromInstance(_scenesConfig).AsSingle();

        }
    }
}
