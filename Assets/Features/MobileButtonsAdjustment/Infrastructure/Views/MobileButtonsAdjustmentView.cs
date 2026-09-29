using System;
using System.Collections.Generic;
using Features.MobileButtonsAdjustment.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.UI.Infrastructure.Buttons;
using UnityEngine;

namespace Features.MobileButtonsAdjustment.Infrastructure.Views
{
    public class MobileButtonsAdjustmentView : MonoBehaviour, IMobileButtonsAdjustmentView, IPanel
    {
        private static readonly PanelMode[] Modes = { PanelMode.MobileButtonAdjustment };

        public event Action Opened;
        public event Action CloseRequested;
        public event Action ResetRequested;
        public event Action<AdjustableButtonType, Vector2> ButtonDragged;

        [SerializeField] private ImageButton _closeMobileAdjustmentButton;
        [SerializeField] private ImageButton _resetAdjustmentButton;
        [SerializeField] private List<AdjustableButtonsMap> _draggableButtons;

        private readonly Dictionary<AdjustableButtonType, DraggableUIElement> _buttons = new();
        private readonly Dictionary<AdjustableButtonType, Vector2> _defaultPositions = new();
        private readonly List<(DraggableUIElement button, Action<Vector2> handler)> _dragHandlers = new();
        private bool _isSetUp;

        public PanelMode[] PanelModes => Modes;
        public PanelInput PanelInput => PanelInput.Mobile;

        public void InitPanel()
        {
            SetUpOnce();
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _closeMobileAdjustmentButton.Click += OnCloseClicked;
            _resetAdjustmentButton.Click += OnResetClicked;
            Opened?.Invoke();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _closeMobileAdjustmentButton.Click -= OnCloseClicked;
            _resetAdjustmentButton.Click -= OnResetClicked;
        }

        public void SetButtonVisible(AdjustableButtonType button, bool isVisible)
        {
            if (_buttons.TryGetValue(button, out var draggable))
                draggable.gameObject.SetActive(isVisible);
        }

        public void SetButtonPosition(AdjustableButtonType button, Vector2 position)
        {
            if (_buttons.TryGetValue(button, out var draggable))
                draggable.SetAnchoredPosition(position);
        }

        public void RestoreDefaultPosition(AdjustableButtonType button)
        {
            if (_buttons.TryGetValue(button, out var draggable))
                draggable.SetAnchoredPosition(_defaultPositions[button]);
        }

        private void OnDestroy()
        {
            foreach (var (button, handler) in _dragHandlers)
                button.Dragged -= handler;
            _dragHandlers.Clear();
        }

        private void SetUpOnce()
        {
            if (_isSetUp) return;
            _isSetUp = true;

            foreach (var map in _draggableButtons)
            {
                if (!_buttons.TryAdd(map.Type, map.DraggableButton))
                    continue;

                _defaultPositions[map.Type] = map.DraggableButton.AnchoredPosition;

                var type = map.Type;
                Action<Vector2> handler = position => ButtonDragged?.Invoke(type, position);
                map.DraggableButton.Dragged += handler;
                _dragHandlers.Add((map.DraggableButton, handler));
            }
        }

        private void OnCloseClicked() => CloseRequested?.Invoke();
        private void OnResetClicked() => ResetRequested?.Invoke();
    }
}
