using Features.Core.InterfaceAdapters.Services;
using Features.Core.UseCases;
using Features.Core.UseCases.Ports;
using Zenject;
using ILogger = Features.Core.UseCases.Ports.ILogger;

namespace Features.Core.Installer
{

    public class CoreInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallInfrastructure();
            InstallAdapters();
            InstallUseCases();
        }
        private void InstallInfrastructure()
        {
            Container.BindInterfacesTo<WorldEnemyHeadRegistry>().AsSingle();
        }

        private void InstallAdapters()
        {
            Container.Bind<ILogger>().To<UnityLogger>().AsSingle();
            Container.Bind<ITimeProvider>().To<UnityTimeProvider>().AsSingle();
        }

        private void InstallUseCases()
        {
            Container.BindInterfacesAndSelfTo<GamePauseService>().AsSingle();
        }


    }
}
