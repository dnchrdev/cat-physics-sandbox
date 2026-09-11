using Features.Storage.Domain;
using Features.Storage.Infrastructure;
using Zenject;

namespace Features.Storage.Installer
{
    public class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AudioSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControlSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileControlsSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProgress>().AsSingle().WithArguments(false).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerStorage>().AsSingle();
            Container.BindInterfacesAndSelfTo<StorageDataService>().AsSingle();
        }
    }
}
