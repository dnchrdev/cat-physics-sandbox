using Features.GameLifeCycle.Application;
using Zenject;

namespace Features.GameLifeCycle.Installer
{
    public class GameLifeCycleInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GameStartedUseCase>().AsSingle().NonLazy();
        }
    }
}