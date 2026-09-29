using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Storage.Entities
{
    public sealed class ControlSettings : IControlSettings
    {
        public const int DefaultLookSensitivity = 50;

        public bool IsMobile { get; private set; }
        public int LookSensitivity { get; private set; }
        public int MinLookSensitivity => 1;
        public int MaxLookSensitivity => 100;

        public ControlSettings()
        {
            ResetToDefaults();
        }

        public void SetIsMobile(bool isMobile) => IsMobile = isMobile;

        public void SetLookSensitivity(int sensitivity) =>
            LookSensitivity = Mathf.Clamp(sensitivity, MinLookSensitivity, MaxLookSensitivity);

        public void ResetToDefaults() => SetLookSensitivity(DefaultLookSensitivity);
    }
}
