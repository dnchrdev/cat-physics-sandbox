using System;
using Features.Player.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Player.InterfaceAdapters.Controllers
{
    public sealed class GameplayControlsController : IInitializable, IDisposable
    {
        private const float MOBILE_ADDITIONAL_SENS_DIVIDER = 50f;

        private readonly IReadOnlyControlSettings _controlSettings;
        private readonly IMovementInput _movementInput;
        private readonly ICameraInput _cameraInput;
        private readonly IInteractionInput _interactionInput;
        private readonly IUIPanelsManager _panels;
        private readonly IMobileControlView _mobileView;
        private readonly IPCGameplayView _pcView;

        public GameplayControlsController(
            IReadOnlyControlSettings controlSettings,
            IMovementInput movementInput,
            ICameraInput cameraInput,
            IInteractionInput interactionInput,
            IUIPanelsManager panels,
            IMobileControlView mobileView,
            IPCGameplayView pcView)
        {
            _controlSettings = controlSettings;
            _movementInput = movementInput;
            _cameraInput = cameraInput;
            _interactionInput = interactionInput;
            _panels = panels;
            _mobileView = mobileView;
            _pcView = pcView;
        }

        public void Initialize()
        {
            if (!_controlSettings.IsMobile)
            {
                _pcView.SettingsOpenedEvent += OpenSettings;
                return;
            }

            _mobileView.SettingsOpenedEvent += OpenSettings;
            _mobileView.MobileButtonsAdjustmentOpenedEvent += OpenMobileAdjustment;
            _mobileView.MoveJoystickUpdatedEvent += _movementInput.InvokeHorizontalMove;
            _mobileView.LookJoystickUpdatedEvent += Look;
            _mobileView.JumpButtonPressedEvent += _movementInput.InvokeJumpStart;
            _mobileView.JumpButtonReleasedEvent += _movementInput.InvokeJumpRelease;
            _mobileView.HitButtonPressedEvent += _interactionInput.InvokeHit;
            _mobileView.GrabButtonPressedEvent += _interactionInput.InvokeGrab;
            _mobileView.ThrowButtonPressedEvent += _interactionInput.InvokeThrow;
            _mobileView.ReleaseButtonPressedEvent += _interactionInput.InvokeRelease;
        }

        public void Dispose()
        {
            if (!_controlSettings.IsMobile)
            {
                _pcView.SettingsOpenedEvent -= OpenSettings;
                return;
            }

            _mobileView.SettingsOpenedEvent -= OpenSettings;
            _mobileView.MobileButtonsAdjustmentOpenedEvent -= OpenMobileAdjustment;
            _mobileView.MoveJoystickUpdatedEvent -= _movementInput.InvokeHorizontalMove;
            _mobileView.LookJoystickUpdatedEvent -= Look;
            _mobileView.JumpButtonPressedEvent -= _movementInput.InvokeJumpStart;
            _mobileView.JumpButtonReleasedEvent -= _movementInput.InvokeJumpRelease;
            _mobileView.HitButtonPressedEvent -= _interactionInput.InvokeHit;
            _mobileView.GrabButtonPressedEvent -= _interactionInput.InvokeGrab;
            _mobileView.ThrowButtonPressedEvent -= _interactionInput.InvokeThrow;
            _mobileView.ReleaseButtonPressedEvent -= _interactionInput.InvokeRelease;
        }

        private void Look(Vector2 delta) =>
            _cameraInput?.InvokeLook(delta * _controlSettings.LookSensitivity / MOBILE_ADDITIONAL_SENS_DIVIDER);

        private void OpenMobileAdjustment() => _panels.OpenPanel(PanelMode.MobileButtonAdjustment);
        private void OpenSettings() => _panels.OpenPanel(PanelMode.Settings);
    }
}
