using Features.Input.Infrastructure;
using Features.Storage.Domain.Interfaces;
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
            if (_controlSettings.IsMobile)
            {
                Container.BindInterfacesTo<MobileInput>().AsSingle();
            }
            else
            {
                Container.BindInterfacesTo<DesktopInput>().AsSingle();
            }
        }
    }
}