using System;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Input.InterfaceAdapters
{
    public sealed class GameInput : ITickable, IMovementInput, ICameraInput, IInteractionInput, IUIPanelInput
    {
        public event Action<Vector2> HorizontalMoveEvent;
        public event Action JumpStartEvent;
        public event Action JumpReleaseEvent;
        public event Action<Vector2> LookEvent;
        public event Action ToggleSettingsEvent;
        public event Action AllQuestsEvent;
        public event Action QuestHintRequestedEvent;

        private readonly OneFramePulse _hit = new();
        private readonly OneFramePulse _grab = new();
        private readonly OneFramePulse _throw = new();
        private readonly OneFramePulse _release = new();

        public bool IsHit => _hit.IsActive;
        public bool IsGrab => _grab.IsActive;
        public bool IsThrow => _throw.IsActive;
        public bool IsRelease => _release.IsActive;

        public void Tick()
        {
            _grab.Tick();
            _throw.Tick();
            _hit.Tick();
            _release.Tick();
        }

        public void InvokeHit() => _hit.Trigger();
        public void InvokeGrab() => _grab.Trigger();
        public void InvokeThrow() => _throw.Trigger();
        public void InvokeRelease() => _release.Trigger();
        public void InvokeHorizontalMove(Vector2 input) => HorizontalMoveEvent?.Invoke(input);
        public void InvokeJumpStart() => JumpStartEvent?.Invoke();
        public void InvokeJumpRelease() => JumpReleaseEvent?.Invoke();
        public void InvokeLook(Vector2 lookDelta) => LookEvent?.Invoke(lookDelta);
        public void ToggleSettings() => ToggleSettingsEvent?.Invoke();
        public void AllQuests() => AllQuestsEvent?.Invoke();
        public void RequestQuestHint() => QuestHintRequestedEvent?.Invoke();

        private sealed class OneFramePulse
        {
            private bool _wasActive;

            public bool IsActive { get; private set; }

            public void Trigger() => IsActive = true;

            public void Tick()
            {
                if (_wasActive)
                    IsActive = false;

                _wasActive = IsActive;
            }
        }
    }
}
