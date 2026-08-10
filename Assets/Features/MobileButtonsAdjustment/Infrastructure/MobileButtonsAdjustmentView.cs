using System;
using System.Collections.Generic;
using Feature.Storage;
using Feature.UI;
using UnityEngine;

namespace Feature.MobileButtonsAdjustment
{
    public class MobileButtonsAdjustmentView : MonoBehaviour, IMobileButtonsAdjustmentView, IPanel
    {
        public event Action CloseRequestedEvent;
        public event Action ResetRequestedEvent;
        public event Action<AdjustableButtonType, Vector2> ButtonDraggedEvent;

        [SerializeField] private ImageButton _closeMobileAdjustmentButton;
        [SerializeField] private ImageButton _resetAdjustmentButton;
        [SerializeField] private List<AdjustableButtonsMap> _draggableButtons;
        
        private readonly Dictionary<DraggableUIElement, Action<Vector2>> _dragHandlers = new();

        public PanelMode[] PanelModes => new[] { PanelMode.MobileButtonAdjustment };
        public PanelInput PanelInput => PanelInput.Mobile;
        public List<AdjustableButtonsMap> AdjustableButtons => _draggableButtons;

        public void InitPanel() => gameObject.SetActive(false);

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            SubscribeButtons();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            UnsubscribeButtons();
            UnsubscribeDragHandlers();
        }

        public void SetActive(bool isActive) => gameObject.SetActive(isActive);

        public void ApplyButtonPositions(IReadOnlyMobileControls settings)
        {
            foreach (var button in _draggableButtons)
            {
                if (settings.GetIsInitialized(button.Type))
                    button.Draggablebutton.GetRectTransform().anchoredPosition =
                        settings.GetAnchoredPosition(button.Type);
            }
        }
        
        public Dictionary<AdjustableButtonType, bool> GetCurrentVisibility()
        {
            var result = new Dictionary<AdjustableButtonType, bool>();

            foreach (var button in _draggableButtons)
                result[button.Type] = button.Button.activeSelf;

            return result;
        }

        public void SetButtonVisibility(AdjustableButtonType type, bool isVisible)
        {
            var button = _draggableButtons.Find(b => b.Type == type);
            button.Draggablebutton.gameObject.SetActive(isVisible);
        }

        public void SetAnchoredPosition(AdjustableButtonType type, Vector2 position)
        {
            var button = _draggableButtons.Find(b => b.Type == type);
            button.Draggablebutton.GetRectTransform().anchoredPosition = position;
        }

        public void SubscribeButtons()
        {
            _closeMobileAdjustmentButton.Click += OnCloseButtonClicked;
            _resetAdjustmentButton.Click += OnResetButtonClicked;
        }

        public void UnsubscribeButtons()
        {
            _closeMobileAdjustmentButton.Click -= OnCloseButtonClicked;
            _resetAdjustmentButton.Click -= OnResetButtonClicked;
        }

        // Presenter передаёт СВОИ данные (visibility) обратно во View,
        // чтобы View подписалась на драг только у видимых кнопок
        public void SubscribeDragHandlers(Dictionary<AdjustableButtonType, bool> visibility)
        {
            foreach (var map in _draggableButtons)
            {
                var isVisible = visibility.TryGetValue(map.Type, out var v) && v;
                map.Draggablebutton.gameObject.SetActive(isVisible);

                if (isVisible == false) continue;

                var draggableButton = map.Draggablebutton;
                var type = map.Type;
                Action<Vector2> handler = pos => ButtonDraggedEvent?.Invoke(type, pos);

                _dragHandlers[draggableButton] = handler;
                draggableButton.OnDragUpdate += handler;
            }
        }

        public void UnsubscribeDragHandlers()
        {
            foreach (var (btn, handler) in _dragHandlers)
                btn.OnDragUpdate -= handler;

            _dragHandlers.Clear();
        }

        private void OnCloseButtonClicked() => CloseRequestedEvent?.Invoke();
        private void OnResetButtonClicked() => ResetRequestedEvent?.Invoke();
    }
}