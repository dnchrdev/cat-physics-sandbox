using System.Collections.Generic;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Storage.Entities
{
    public sealed class MobileControlsSettings : IMobileControlsSettings
    {
        private const int DefaultJoystickRadius = 125;

        private readonly Dictionary<AdjustableButtonType, Vector2> _customPositions = new();

        public int JoystickRadius { get; private set; }
        public bool IsDynamicJoystick { get; private set; }
        public bool IsFollowJoystick { get; private set; }
        public int MinJoystickRadius => 50;
        public int MaxJoystickRadius => 250;
        public bool HasCustomPositions => _customPositions.Count > 0;

        public MobileControlsSettings()
        {
            ResetToDefaults();
        }

        public bool TryGetCustomPosition(AdjustableButtonType button, out Vector2 position) =>
            _customPositions.TryGetValue(button, out position);

        public void SetCustomPosition(AdjustableButtonType button, Vector2 position) =>
            _customPositions[button] = position;

        public void ResetPositions() => _customPositions.Clear();

        public void SetJoystickRadius(int radius) =>
            JoystickRadius = Mathf.Clamp(radius, MinJoystickRadius, MaxJoystickRadius);

        public void SetDynamicJoystick(bool isDynamic) => IsDynamicJoystick = isDynamic;

        public void SetFollowJoystick(bool follow) => IsFollowJoystick = follow;

        public void ResetToDefaults()
        {
            SetJoystickRadius(DefaultJoystickRadius);
            IsDynamicJoystick = true;
            IsFollowJoystick = true;
            ResetPositions();
        }
    }
}
