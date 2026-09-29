using System;

namespace Features.Shared.Entities
{
    [Serializable]
    public struct PanelState
    {
        public PanelMode PanelMode;
        public bool IsCursorVisible;
        public CursorLockMode LockState;
        public bool IsPaused;
    }
}
