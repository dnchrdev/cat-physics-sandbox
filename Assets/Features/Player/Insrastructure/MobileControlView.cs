using System;
using System.Collections.Generic;
using Feature.MobileButtonsAdjustment;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
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

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.Mobile;

        [SerializeField] private ImageButton _openSettingsButton;
        [SerializeField] private ImageButton _openMobileAdjustmentButton;
        [SerializeField] private Joystick _joystickMove;
        [SerializeField] private List<ScreenDragPanel> _joystickLook;
        [SerializeField] private ImageButton _jumpButton;
        [SerializeField] private ImageButton _hitButton;
        [SerializeField] private ImageButton _grabButton;
        [SerializeField] private ImageButton _throwButton;
        [SerializeField] private ImageButton _releaseButton;

        [Inject] private readonly IReadOnlyMobileControls _mobileControlsSettings;

        private Dictionary<AdjustableButtonType, ImageButton> _adjustableButtons;

        public void InitPanel()
        {
            _adjustableButtons = new Dictionary<AdjustableButtonType, ImageButton>
            {
                { AdjustableButtonType.Jump, _jumpButton },
                { AdjustableButtonType.Hit, _hitButton },
                { AdjustableButtonType.Grab, _grabButton },
                { AdjustableButtonType.Throw, _throwButton },
                { AdjustableButtonType.Release, _releaseButton },
            };
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            InitMoveJoystick(_mobileControlsSettings);
            ApplyButtonPositions(_mobileControlsSettings);

            SubscribeButtons();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            UnsubscribeButtons();
        }

        public void InitMoveJoystick(IReadOnlyMobileControls settings)
        {
            var type = AdjustableButtonType.MoveJoystick;

            if (settings.GetIsInitialized(type))
                _joystickMove.UpdateValues(settings.GetAnchoredPosition(type),
                    settings.JoystickRadius, settings.IsDynamicJoystick, settings.IsFollowJoystick);
            else
                _joystickMove.UpdateValues(settings.JoystickRadius,
                    settings.IsDynamicJoystick, settings.IsFollowJoystick);
        }

        public void ApplyButtonPositions(IReadOnlyMobileControls settings)
        {
            foreach (var (type, button) in _adjustableButtons)
            {
                if (settings.GetIsInitialized(type))
                    button.SetAnchoredPosition(settings.GetAnchoredPosition(type));
            }
        }

        private void SubscribeButtons()
        {
            _openSettingsButton.Click += OnSettingsButtonClicked;
            _openMobileAdjustmentButton.Click += OnMobileAdjustmentButtonClicked;

            _joystickMove.OnValueChanged += OnMoveJoystickUpdated;

            foreach (var dragPanel in _joystickLook)
            {
                dragPanel.GetValueEvent += HandleLookJoystickValue;
            }

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
            {
                dragPanel.GetValueEvent -= HandleLookJoystickValue;
            }

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
        private void HandleLookJoystickValue(Vector2 vector) => LookJoystickUpdatedEvent?.Invoke(vector);
        private void OnJumpButtonPressed() => JumpButtonPressedEvent?.Invoke();
        private void OnJumpButtonReleased() => JumpButtonReleasedEvent?.Invoke();
        private void OnHitButtonPressed() => HitButtonPressedEvent?.Invoke();
        private void OnGrabButtonPressed() => GrabButtonPressedEvent?.Invoke();
        private void OnThrowButtonPressed() => ThrowButtonPressedEvent?.Invoke();
        private void OnReleaseButtonPressed() => ReleaseButtonPressedEvent?.Invoke();
    }
}