using Features.Interaction.Entities;

namespace Features.Interaction.InterfaceAdapters.Ports
{
    public interface IInteractionHintsShower
    {
        void ShowHint(InteractionType type);
        void ShowAfterGrabHint();
        void CloseAllHints();
    }
}
