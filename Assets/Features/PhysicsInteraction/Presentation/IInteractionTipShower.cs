using System.Collections;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public interface IInteractionTipShower
    {
        void ShowTip(InteractionType type);
        void ShowAfterGrabTip();
        void CloseAllTips();
    }
}