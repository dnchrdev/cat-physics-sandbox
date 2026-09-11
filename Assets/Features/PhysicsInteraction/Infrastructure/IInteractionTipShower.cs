using Features.PhysicsInteraction.Infrastructure.Data;

namespace Features.PhysicsInteraction.Infrastructure
{
    public interface IInteractionTipShower
    {
        void ShowHint(InteractionType type);
        void ShowAfterGrabHint();
        void CloseAllHints();
    }
}