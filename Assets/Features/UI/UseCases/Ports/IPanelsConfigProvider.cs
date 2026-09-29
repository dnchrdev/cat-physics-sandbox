using Features.Shared.Entities;

namespace Features.UI.UseCases.Ports
{
    public interface IPanelsConfigProvider
    {
        PanelState GetPanelConfig(PanelMode mode);
    }
}
