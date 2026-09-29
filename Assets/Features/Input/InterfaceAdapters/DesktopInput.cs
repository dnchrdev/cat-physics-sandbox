using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Features.Input.InterfaceAdapters
{
    public sealed class DesktopInput : IInitializable, IDisposable, ITickable
    {
        private readonly GameInput _input;
        private IA_Player _actions;

        public DesktopInput(GameInput input)
        {
            _input = input;
        }

        public void Initialize()
        {
            _actions = new IA_Player();
            _actions.Enable();

            var keyboard = _actions.Keyboard;
            keyboard.Jump.started += OnJumpStarted;
            keyboard.Jump.canceled += OnJumpCanceled;
            keyboard.Scratch.started += OnHitStarted;
            keyboard.Grab.started += OnGrabStarted;
            keyboard.Throw.started += OnThrowStarted;
            keyboard.Release.started += OnReleaseStarted;
            keyboard.ToggleSettings.started += OnToggleSettingsStarted;
            keyboard.AllQuests.started += OnAllQuestsStarted;
            keyboard.QuestAdv.started += OnQuestHintStarted;
        }

        public void Dispose()
        {
            var keyboard = _actions.Keyboard;
            keyboard.Jump.started -= OnJumpStarted;
            keyboard.Jump.canceled -= OnJumpCanceled;
            keyboard.Scratch.started -= OnHitStarted;
            keyboard.Grab.started -= OnGrabStarted;
            keyboard.Throw.started -= OnThrowStarted;
            keyboard.Release.started -= OnReleaseStarted;
            keyboard.ToggleSettings.started -= OnToggleSettingsStarted;
            keyboard.AllQuests.started -= OnAllQuestsStarted;
            keyboard.QuestAdv.started -= OnQuestHintStarted;

            _actions.Disable();
            _actions.Dispose();
        }

        public void Tick()
        {
            _input.InvokeHorizontalMove(_actions.Keyboard.Move.ReadValue<Vector2>());
            _input.InvokeLook(_actions.Keyboard.Look.ReadValue<Vector2>());
        }

        private void OnJumpStarted(InputAction.CallbackContext context) => _input.InvokeJumpStart();
        private void OnJumpCanceled(InputAction.CallbackContext context) => _input.InvokeJumpRelease();
        private void OnHitStarted(InputAction.CallbackContext context) => _input.InvokeHit();
        private void OnGrabStarted(InputAction.CallbackContext context) => _input.InvokeGrab();
        private void OnThrowStarted(InputAction.CallbackContext context) => _input.InvokeThrow();
        private void OnReleaseStarted(InputAction.CallbackContext context) => _input.InvokeRelease();
        private void OnToggleSettingsStarted(InputAction.CallbackContext context) => _input.ToggleSettings();
        private void OnAllQuestsStarted(InputAction.CallbackContext context) => _input.AllQuests();
        private void OnQuestHintStarted(InputAction.CallbackContext context) => _input.RequestQuestHint();
    }
}
