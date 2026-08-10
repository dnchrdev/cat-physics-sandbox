using System;
using System.Collections.Generic;
using Feature.Storage;
using Feature.UI;
using UnityEngine;

namespace Feature.MobileButtonsAdjustment
{
    public interface IMobileButtonsAdjustmentView
    {
        event Action CloseRequestedEvent;
        event Action ResetRequestedEvent;
        event Action<AdjustableButtonType, Vector2> ButtonDraggedEvent;

        List<AdjustableButtonsMap> AdjustableButtons { get; }

        void SetActive(bool isActive);
        void ApplyButtonPositions(IReadOnlyMobileControls settings);
        void SetAnchoredPosition(AdjustableButtonType type, Vector2 position);
        void SetButtonVisibility(AdjustableButtonType type, bool isVisible);

        Dictionary<AdjustableButtonType, bool> GetCurrentVisibility();

        void SubscribeButtons();
        void UnsubscribeButtons();
        void SubscribeDragHandlers(Dictionary<AdjustableButtonType, bool> visibility);
        void UnsubscribeDragHandlers();
    }
}