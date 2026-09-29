using Features.GameBootstrap.InterfaceAdapters;
using Features.GameBootstrap.UseCases;
using Zenject;

namespace Features.GameBootstrap.Installer
{
    public class GameBootstrapInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallAdapters();
            InstallUseCases();
        }
        
        private void InstallAdapters()
        {
            Container.BindInterfacesTo<GameBootstrapController>().AsSingle();
        }
        
        private void InstallUseCases()
        {
            Container.Bind<GameLaunchUseCase>().AsSingle();
        }
    }
}
