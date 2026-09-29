using UnityEngine;

namespace Features.Interaction.InterfaceAdapters.Ports
{
    public interface IInteractionIndicator
    {
        void ShowIndication();
        void HideIndication();
        void ShowFocusedIndication();
        void ShowGrabbedIndication();
        void UpdateIndicationPosition(Vector3 screenPosition);
    }
}
