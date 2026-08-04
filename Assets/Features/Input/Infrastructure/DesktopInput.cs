using System;
using UnityEngine;
using Zenject;

namespace Feature.Input
{
    public class DesktopInput : ITickable, IInitializable, IDisposable, IMovementInput, ICameraInput, IInteractionInput, IUIPanelInput
    {
        private IA_Player _actions;

        public event Action<Vector2> HorizontalMoveEvent;

        public event Action JumpStartEvent;
        public event Action JumpReleaseEvent;

        public event Action<Vector2> LookEvent;

        public event Action ToggleSettingsEvent;
        public event Action AllQuestsEvent;
        public event Action QuestHintsEvent;

        private bool _prevHit;
        public bool IsHit { get; private set; }
        private bool _prevGrab;
        public bool IsGrab { get; private set; }
        private bool _prevThrow;
        public bool IsThrow { get; private set; }
        private bool _prevRelease;
        public bool IsRelease { get; private set; }

        public void Initialize()
        {
            _actions = new IA_Player();
            _actions.Enable();

            _actions.Keyboard.Jump.started += ctx => JumpStartEvent?.Invoke();
            _actions.Keyboard.Jump.canceled += ctx => JumpReleaseEvent?.Invoke();

            _actions.Keyboard.Scratch.started += ctx => InvokeHit();
            _actions.Keyboard.Grab.started += ctx => InvokeGrab();
            _actions.Keyboard.Throw.started += ctx => InvokeThrow();
            _actions.Keyboard.Release.started += ctx => InvokeRelease();

            _actions.Keyboard.ToggleSettings.started += ctx => ToggleSettingsEvent?.Invoke();
            _actions.Keyboard.AllQuests.started += ctx => AllQuestsEvent?.Invoke();
            _actions.Keyboard.QuestAdv.started += ctx => QuestHintsEvent?.Invoke();
        }
        
        public void Dispose()
        {
            _actions.Keyboard.Jump.started -= ctx => JumpStartEvent?.Invoke();
            _actions.Keyboard.Jump.canceled -= ctx => JumpReleaseEvent?.Invoke();

            _actions.Keyboard.Scratch.started -= ctx => InvokeHit();
            _actions.Keyboard.Grab.started -= ctx => InvokeGrab();
            _actions.Keyboard.Throw.started -= ctx => InvokeThrow();
            _actions.Keyboard.Release.started -= ctx => InvokeRelease();

            _actions.Keyboard.ToggleSettings.started -= ctx => ToggleSettingsEvent?.Invoke();
            _actions.Keyboard.ToggleSettings.started -= ctx => ToggleSettingsEvent?.Invoke();
            _actions.Keyboard.AllQuests.started -= ctx => AllQuestsEvent?.Invoke();
            _actions.Keyboard.QuestAdv.started -= ctx => QuestHintsEvent?.Invoke();
            
            _actions.Disable();
            _actions.Dispose();
        }

        public void Tick()
        {
            HorizontalMoveEvent?.Invoke(_actions.Keyboard.Move.ReadValue<Vector2>());
            LookEvent?.Invoke(_actions.Keyboard.Look.ReadValue<Vector2>());

            if(_prevGrab) IsGrab = false;
            if(_prevThrow) IsThrow = false;
            if (_prevHit) IsHit = false;
            if(_prevRelease) IsRelease = false;

            _prevGrab = IsGrab;
            _prevThrow = IsThrow;
            _prevHit = IsHit;
            _prevRelease = IsRelease;
        }

        public void InvokeHorizontalMove(Vector2 moveInput)
        {
            HorizontalMoveEvent?.Invoke(moveInput);
        }

        public void InvokeJumpStart()
        {
            JumpStartEvent?.Invoke();
        }

        public void InvokeJumpRelease()
        {
            JumpReleaseEvent?.Invoke();
        }
        public void InvokeLook(Vector2 lookDelta)
        {
            LookEvent?.Invoke(lookDelta);
        }

        public void InvokeHit()
        {
            IsHit = true;
        }

        public void InvokeGrab()
        {
            IsGrab = true;
        }

        public void InvokeThrow()
        {
            IsThrow = true;
        }

        public void InvokeRelease()
        {
            IsRelease = true;
        }

        public void ToggleSettings()
        {
            ToggleSettingsEvent?.Invoke();
        }

        public void AllQuests()
        {
            AllQuestsEvent?.Invoke();
        }

        public void QuestTip()
        {
            QuestHintsEvent?.Invoke();
        }
    }
}
