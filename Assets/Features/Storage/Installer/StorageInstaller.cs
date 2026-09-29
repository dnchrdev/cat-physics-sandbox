using Features.Storage.Entities;
using Features.Storage.Infrastructure;
using Features.Storage.UseCases;
using Zenject;
using AudioSettings = Features.Storage.Entities.AudioSettings;

namespace Features.Storage.Installer
{
    public sealed class StorageInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallUseCases();
            InstallEntities();
        }

        private void InstallInfrastructure()
        {
            Container.BindInterfacesAndSelfTo<PlayerStorage>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.BindInterfacesAndSelfTo<StorageDataService>().AsSingle();
        }

        private void InstallEntities()
        {
            Container.BindInterfacesAndSelfTo<ButtonVisibilityState>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<ControlSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<MobileControlsSettings>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerProgress>().AsSingle();
        }
    }
}
