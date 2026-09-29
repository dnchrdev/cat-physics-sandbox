using Features.Shared.Entities;

namespace Features.Storage.Entities.Ports
{
    public interface IButtonVisibilityWriter
    {
        void SetVisible(AdjustableButtonType type, bool visible);
    }
}
