using System;
using System.Collections.Generic;
using Feature.Input;
using Feature.MobileButtonsAdjustment;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class MobileControlPresenter : MonoBehaviour, IPanel
    {
        public Action OpenSettingsEvent;
        public Action OpenMobileAdjustmentEvent;
        public Action OnPanelExit;

        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
        {
            UIPanelTag.Gameplay
        };


        [SerializeField] private ImageButton _openSettingsButton;
        [SerializeField] private ImageButton _openMobileAdjustmentButton;
        [SerializeField] private Joystick _joystickMove;
        [SerializeField] private List<DeltaDragPanel> _joystickLook;

        [SerializeField] private ImageButton _jumpButton;

        [SerializeField] private ImageButton _hitButton;
        [SerializeField] private ImageButton _grabButton;
        [SerializeField] private ImageButton _throwButton;
        [SerializeField] private ImageButton _releasepButton;

        private IReadOnlyControlSettings _controlSettings;
        private IReadOnlyMobileControls _mobileControlsSettings;
        private IMovementInput _movementInput;
        private ICameraInput _cameraInput;
        private IInteractionInput _interactionInput;

        [Inject]
        private void Construct(
            IMovementInput movementInput,
            ICameraInput cameraInput,
            IInteractionInput interactionInput,
            IReadOnlyControlSettings controlSettings,
            IReadOnlyMobileControls mobileControlsSettings)
        {
            _movementInput = movementInput;
            _cameraInput = cameraInput;
            _interactionInput = interactionInput;
            _controlSettings = controlSettings;
            _mobileControlsSettings = mobileControlsSettings;
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _openSettingsButton.Click += HandleOpenSettings;
            _openMobileAdjustmentButton.Click += HandleMobileAdjustment;

            _joystickMove.OnValueChanged += _movementInput.InvokeHorizontalMove;

            var type = AdjustableButtonType.MoveJoystick;

            if (_mobileControlsSettings.GetIsInitialized(type))
                _joystickMove.UpdateValues(_mobileControlsSettings.GetAnchoredPosition(type),
                    _mobileControlsSettings.JoystickRadius, _mobileControlsSettings.IsDynamicJoystick,
                    _mobileControlsSettings.IsFollowJoystick);
            else
                _joystickMove.UpdateValues(_mobileControlsSettings.JoystickRadius,
                    _mobileControlsSettings.IsDynamicJoystick, _mobileControlsSettings.IsFollowJoystick);

            foreach (var kvp in _joystickLook)
            {
                kvp.GetValueEvent += HandleLookJoystickValue;
            }

            SetButtonPosition(_jumpButton, AdjustableButtonType.Jump);
            _jumpButton.Down += _movementInput.InvokeJumpStart;
            _jumpButton.Up += _movementInput.InvokeJumpRelease;

            SetButtonPosition(_hitButton, AdjustableButtonType.Hit);
            _hitButton.Down += _interactionInput.InvokeHit;

            SetButtonPosition(_grabButton, AdjustableButtonType.Grab);
            _grabButton.Down += _interactionInput.InvokeGrab;

            SetButtonPosition(_throwButton, AdjustableButtonType.Throw);
            _throwButton.Down += _interactionInput.InvokeThrow;

            SetButtonPosition(_releasepButton, AdjustableButtonType.Release);
            _releasepButton.Down += _interactionInput.InvokeRelease;
        }

        private void SetButtonPosition(ImageButton button, AdjustableButtonType type)
        {
            if (_mobileControlsSettings.GetIsInitialized(type))
                button.SetAnchoredPosition(_mobileControlsSettings.GetAnchoredPosition(type));
        }

        private void HandleLookJoystickValue(Vector2 vector)
        {
            _cameraInput?.InvokeLook(vector * _controlSettings.LookSensitivity / 50f);
        }

        private void HandleMobileAdjustment()
        {
            OpenMobileAdjustmentEvent?.Invoke();
        }

        private void HandleOpenSettings()
        {
            OpenSettingsEvent?.Invoke();
        }

        public void OnExitPanel()
        {
            OnPanelExit?.Invoke();

            gameObject.SetActive(false);

            _joystickMove.OnValueChanged -= _movementInput.InvokeHorizontalMove;

            foreach (var kvp in _joystickLook)
            {
                kvp.GetValueEvent -= HandleLookJoystickValue;
            }

            _jumpButton.Down -= _movementInput.InvokeJumpStart;
            _jumpButton.Up -= _movementInput.InvokeJumpRelease;

            _hitButton.Down -= _interactionInput.InvokeHit;
            _grabButton.Down -= _interactionInput.InvokeGrab;

            _throwButton.Down -= _interactionInput.InvokeThrow;
            _releasepButton.Down -= _interactionInput.InvokeRelease;
        }

        public void Tick(float dt)
        {
        }
    }
}