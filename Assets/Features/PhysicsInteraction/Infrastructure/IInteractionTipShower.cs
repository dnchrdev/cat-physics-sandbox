using System.Collections;
using Feature.UI;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IInteractionTipShower
    {
        void ShowHint(InteractionType type);
        void ShowAfterGrabHint();
        void CloseAllHints();
    }
}