using System;
using Feature.Input;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class GameplayControlPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly IReadOnlyControlSettings _controlSettings;
        [Inject] private readonly IMovementInput _movementInput;
        [Inject] private readonly ICameraInput _cameraInput;
        [Inject] private readonly IInteractionInput _interactionInput;
        [Inject] private readonly UIPanelsManager _panelManager;
        [Inject] private readonly IMobileControlView _mobileView;
        [Inject] private readonly IPCGameplayView _pcView;
        
        public void Initialize()
        {
            SubscribeToView();
        }

        public void Dispose()
        {
            UnsubscribeFromView();
        }

        private void SubscribeToView()
        {
            if (_controlSettings.IsMobile)
            {
                _mobileView.SettingsOpenedEvent += HandleOpenSettings;
                _mobileView.MobileButtonsAdjustmentOpenedEvent += HandleMobileAdjustment;

                _mobileView.MoveJoystickUpdatedEvent += _movementInput.InvokeHorizontalMove;
                _mobileView.LookJoystickUpdatedEvent += HandleLookValue;

                _mobileView.JumpButtonPressedEvent += _movementInput.InvokeJumpStart;
                _mobileView.JumpButtonReleasedEvent += _movementInput.InvokeJumpRelease;

                _mobileView.HitButtonPressedEvent += _interactionInput.InvokeHit;
                _mobileView.GrabButtonPressedEvent += _interactionInput.InvokeGrab;
                _mobileView.ThrowButtonPressedEvent += _interactionInput.InvokeThrow;
                _mobileView.ReleaseButtonPressedEvent += _interactionInput.InvokeRelease;
            }
            else
            {
                _pcView.SettingsOpenedEvent += HandleOpenSettings;
            }
        }

        private void UnsubscribeFromView()
        {
            if (_controlSettings.IsMobile)
            {
                _mobileView.SettingsOpenedEvent -= HandleOpenSettings;
                _mobileView.MobileButtonsAdjustmentOpenedEvent -= HandleMobileAdjustment;

                _mobileView.MoveJoystickUpdatedEvent -= _movementInput.InvokeHorizontalMove;
                _mobileView.LookJoystickUpdatedEvent -= HandleLookValue;

                _mobileView.JumpButtonPressedEvent -= _movementInput.InvokeJumpStart;
                _mobileView.JumpButtonReleasedEvent -= _movementInput.InvokeJumpRelease;

                _mobileView.HitButtonPressedEvent -= _interactionInput.InvokeHit;
                _mobileView.GrabButtonPressedEvent -= _interactionInput.InvokeGrab;
                _mobileView.ThrowButtonPressedEvent -= _interactionInput.InvokeThrow;
                _mobileView.ReleaseButtonPressedEvent -= _interactionInput.InvokeRelease;
            }
            else
            {
                _pcView.SettingsOpenedEvent -= HandleOpenSettings;
            }
        }

        private void HandleLookValue(Vector2 vector) =>
            _cameraInput?.InvokeLook(vector * _controlSettings.LookSensitivity / 50f);

        private void HandleMobileAdjustment() =>
            _panelManager.OpenPanel(PanelMode.MobileButtonAdjustment);

        private void HandleOpenSettings() =>
            _panelManager.OpenPanel(PanelMode.Settings);

    }
}