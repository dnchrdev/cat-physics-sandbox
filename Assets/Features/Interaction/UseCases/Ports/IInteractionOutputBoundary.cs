using Features.Interaction.Entities;
using UnityEngine;

namespace Features.Interaction.UseCases.Ports
{
    public interface IInteractionOutputBoundary
    {
        void ShowHint(InteractionType type);
        void ShowAfterGrabHint();
        void CloseHints();

        void ShowIndication(IndicationType type);
        void MoveIndication(Vector3 worldPoint);
        void HideIndication();
    }
}
