using System;
using System.Collections.Generic;
using Features.Player.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;
using Features.Storage.Entities.Ports;
using Features.UI.Infrastructure.Buttons;
using Features.UI.Infrastructure.MobileControls;
using UnityEngine;
using Zenject;

namespace Features.Player.Infrastructure.Views
{
    public class MobileControlView : MonoBehaviour, IMobileControlView, IPanel
    {
        public event Action SettingsOpenedEvent;
        public event Action MobileButtonsAdjustmentOpenedEvent;
        public event Action<Vector2> MoveJoystickUpdatedEvent;
        public event Action<Vector2> LookJoystickUpdatedEvent;
        public event Action JumpButtonPressedEvent;
        public event Action JumpButtonReleasedEvent;
        public event Action HitButtonPressedEvent;
        public event Action GrabButtonPressedEvent;
        public event Action ThrowButtonPressedEvent;
        public event Action ReleaseButtonPressedEvent;

        [SerializeField] private ImageButton _openSettingsButton;
        [SerializeField] private ImageButton _openMobileAdjustmentButton;
        [SerializeField] private Joystick _joystickMove;
        [SerializeField] private List<ScreenDragPanel> _joystickLook;
        [SerializeField] private ImageButton _jumpButton;
        [SerializeField] private ImageButton _hitButton;
        [SerializeField] private ImageButton _grabButton;
        [SerializeField] private ImageButton _throwButton;
        [SerializeField] private ImageButton _releaseButton;

        private IReadOnlyMobileControls _mobileControlsSettings;
        private IButtonVisibilityReader _buttonVisibility;
        private Dictionary<AdjustableButtonType, ImageButton> _adjustableButtons;

        private readonly Dictionary<AdjustableButtonType, Vector2> _defaultPositions = new();
        private Vector2 _defaultJoystickPosition;
        private bool _isDefaultLayoutCaptured;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.Mobile;

        [Inject]
        private void Construct(IReadOnlyMobileControls mobileControlsSettings,
                               IButtonVisibilityReader buttonVisibility)
        {
            _mobileControlsSettings = mobileControlsSettings;
            _buttonVisibility = buttonVisibility;
        }

        public void InitPanel()
        {
            _adjustableButtons = new Dictionary<AdjustableButtonType, ImageButton>
            {
                { AdjustableButtonType.Jump,    _jumpButton },
                { AdjustableButtonType.Hit,     _hitButton },
                { AdjustableButtonType.Grab,    _grabButton },
                { AdjustableButtonType.Throw,   _throwButton },
                { AdjustableButtonType.Release, _releaseButton },
            };

            CaptureDefaultLayout();
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            InitMoveJoystick(_mobileControlsSettings);
            ApplyButtonPositions(_mobileControlsSettings);
            ApplyAllVisibilities();
            _buttonVisibility.Changed += OnButtonVisibilityChanged;
            SubscribeButtons();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _buttonVisibility.Changed -= OnButtonVisibilityChanged;
            UnsubscribeButtons();
        }

        private void CaptureDefaultLayout()
        {
            if (_isDefaultLayoutCaptured) return;
            _isDefaultLayoutCaptured = true;

            foreach (var (type, button) in _adjustableButtons)
                _defaultPositions[type] = ((RectTransform)button.transform).anchoredPosition;

            _defaultJoystickPosition = ((RectTransform)_joystickMove.transform).anchoredPosition;
        }

        private void InitMoveJoystick(IReadOnlyMobileControls settings)
        {
            var position = settings.TryGetCustomPosition(AdjustableButtonType.MoveJoystick, out var custom)
                ? custom
                : _defaultJoystickPosition;

            _joystickMove.UpdateValues(
                position,
                settings.JoystickRadius,
                settings.IsDynamicJoystick,
                settings.IsFollowJoystick);
        }

        private void ApplyButtonPositions(IReadOnlyMobileControls settings)
        {
            foreach (var (type, button) in _adjustableButtons)
            {
                var position = settings.TryGetCustomPosition(type, out var custom)
                    ? custom
                    : _defaultPositions[type];

                button.SetAnchoredPosition(position);
            }
        }

        private void ApplyAllVisibilities()
        {
            foreach (var (type, button) in _adjustableButtons)
                button.gameObject.SetActive(_buttonVisibility.GetVisible(type));
        }

        private void OnButtonVisibilityChanged(AdjustableButtonType type, bool visible)
        {
            if (_adjustableButtons.TryGetValue(type, out var button))
                button.gameObject.SetActive(visible);
        }

        private void SubscribeButtons()
        {
            _openSettingsButton.Click += OnSettingsButtonClicked;
            _openMobileAdjustmentButton.Click += OnMobileAdjustmentButtonClicked;
            _joystickMove.OnValueChanged += OnMoveJoystickUpdated;

            foreach (var dragPanel in _joystickLook)
                dragPanel.GetValueEvent += OnLookJoystickUpdated;

            _jumpButton.Down += OnJumpButtonPressed;
            _jumpButton.Up += OnJumpButtonReleased;
            _hitButton.Down += OnHitButtonPressed;
            _grabButton.Down += OnGrabButtonPressed;
            _throwButton.Down += OnThrowButtonPressed;
            _releaseButton.Down += OnReleaseButtonPressed;
        }

        private void UnsubscribeButtons()
        {
            _openSettingsButton.Click -= OnSettingsButtonClicked;
            _openMobileAdjustmentButton.Click -= OnMobileAdjustmentButtonClicked;
            _joystickMove.OnValueChanged -= OnMoveJoystickUpdated;

            foreach (var dragPanel in _joystickLook)
                dragPanel.GetValueEvent -= OnLookJoystickUpdated;

            _jumpButton.Down -= OnJumpButtonPressed;
            _jumpButton.Up -= OnJumpButtonReleased;
            _hitButton.Down -= OnHitButtonPressed;
            _grabButton.Down -= OnGrabButtonPressed;
            _throwButton.Down -= OnThrowButtonPressed;
            _releaseButton.Down -= OnReleaseButtonPressed;
        }

        private void OnSettingsButtonClicked() => SettingsOpenedEvent?.Invoke();
        private void OnMobileAdjustmentButtonClicked() => MobileButtonsAdjustmentOpenedEvent?.Invoke();
        private void OnMoveJoystickUpdated(Vector2 vector) => MoveJoystickUpdatedEvent?.Invoke(vector);
        private void OnLookJoystickUpdated(Vector2 vector) => LookJoystickUpdatedEvent?.Invoke(vector);
        private void OnJumpButtonPressed() => JumpButtonPressedEvent?.Invoke();
        private void OnJumpButtonReleased() => JumpButtonReleasedEvent?.Invoke();
        private void OnHitButtonPressed() => HitButtonPressedEvent?.Invoke();
        private void OnGrabButtonPressed() => GrabButtonPressedEvent?.Invoke();
        private void OnThrowButtonPressed() => ThrowButtonPressedEvent?.Invoke();
        private void OnReleaseButtonPressed() => ReleaseButtonPressedEvent?.Invoke();
    }
}
