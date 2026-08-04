using System;
using UnityEngine;
using Zenject;

namespace Feature.Input
{
    public class MobileInput : ITickable, IMovementInput, ICameraInput, IInteractionInput, IUIPanelInput
    {
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
        public bool _prevRelease;
        public bool IsRelease { get; private set; }

        public void Tick()
        {
            if (_prevGrab) IsGrab = false;
            if (_prevThrow) IsThrow = false;
            if (_prevHit) IsHit = false;
            if (_prevRelease) IsRelease = false;

            _prevGrab = IsGrab;
            _prevThrow = IsThrow;
            _prevHit = IsHit;
            _prevRelease = IsRelease;
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

        public void InvokeHorizontalMove(Vector2 input)
        {
            HorizontalMoveEvent?.Invoke(input);
        }

        public void InvokeJumpRelease()
        {
            JumpReleaseEvent?.Invoke();
        }

        public void InvokeJumpStart()
        {
            JumpStartEvent?.Invoke();
        }

        public void InvokeLook(Vector2 lookDelta)
        {
            LookEvent?.Invoke(lookDelta);
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
