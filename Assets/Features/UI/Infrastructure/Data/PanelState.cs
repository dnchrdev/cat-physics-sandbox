using System;
using UnityEngine;

namespace Features.UI.Infrastructure.Data
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