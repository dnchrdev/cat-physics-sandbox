using Features.GameLifeCycle.InterfaceAdapters;
using Features.GameLifeCycle.UseCases;
using Zenject;

namespace Features.GameLifeCycle.Installer
{
    public class GameLifeCycleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallAdapters();
            InstallUseCases();
        }

        private void InstallAdapters()
        {
            Container.BindInterfacesTo<GameLifeCycleController>().AsSingle();
        }
        
        private void InstallUseCases()
        {
            Container.Bind<GameLifeCycleUseCase>().AsSingle();
        }
    }
}
