using Features.Input.InterfaceAdapters;
using Features.Shared.Entities.Ports;
using Zenject;

namespace Features.Input.Installer
{
    public class InputInstaller : MonoInstaller
    {
        private IReadOnlyControlSettings _controlSettings;

        [Inject]
        private void Construct(IReadOnlyControlSettings controlSettings)
        {
            _controlSettings = controlSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameInput>().AsSingle();

            if (!_controlSettings.IsMobile)
                Container.BindInterfacesTo<DesktopInput>().AsSingle();
        }
    }
}
