using System;
using UnityEngine;

namespace Feature.UI
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