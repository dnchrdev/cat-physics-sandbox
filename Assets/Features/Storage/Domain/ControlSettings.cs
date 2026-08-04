using UnityEngine;

namespace Feature.Storage
{
    public class ControlSettings : IReadOnlyControlSettings
    {
        public bool IsMobile { get; private set; }
        public int LookSensitivity { get; private set; }

        public int MinLookSensitivity => _minLookSensitivity;
        public int MaxLookSensitivity => _maxLookSensitivity;

        private readonly int _minLookSensitivity;
        private readonly int _maxLookSensitivity;

        public ControlSettings(bool isMobile = false, int lookSensitivity = 50, int minLookSensitivity = 1, int maxLookSensitivity = 100)
        {
            SetIsMobile(isMobile);
            _minLookSensitivity = Mathf.Max(minLookSensitivity, 0);
            _maxLookSensitivity = Mathf.Max(maxLookSensitivity, _minLookSensitivity);
            SetLookSensitivity(lookSensitivity);
        }

        public void SetIsMobile(bool isMobile)
        {
            IsMobile = isMobile;
        }

        public void SetLookSensitivity(int sensitivity)
        {
            LookSensitivity = Mathf.Clamp(sensitivity, _minLookSensitivity, _maxLookSensitivity);
        }
    }
}