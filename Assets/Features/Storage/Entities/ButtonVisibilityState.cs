using System;
using System.Collections.Generic;
using Features.Shared.Entities;
using Features.Storage.Entities.Ports;

namespace Features.Storage.Entities
{
    public sealed class ButtonVisibilityState: IButtonVisibilityWriter, IButtonVisibilityReader
    {
        private readonly Dictionary<AdjustableButtonType, bool> _visibility = new();

        public event Action<AdjustableButtonType, bool> Changed;

        public ButtonVisibilityState()
        {
            _visibility.Add(AdjustableButtonType.MoveJoystick, true);
            _visibility.Add(AdjustableButtonType.Jump, true);
        }

        public void SetVisible(AdjustableButtonType type, bool visible)
        {
            if (_visibility.TryGetValue(type, out var current) && current == visible) return;
            _visibility[type] = visible;
            Changed?.Invoke(type, visible);
        }

        public bool GetVisible(AdjustableButtonType type) =>
            _visibility.TryGetValue(type, out var v) ? v : true;

        public IReadOnlyDictionary<AdjustableButtonType, bool> GetAll() => _visibility;
    }
}
