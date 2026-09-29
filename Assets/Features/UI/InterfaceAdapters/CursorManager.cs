using Features.Shared.UseCases;
using UnityEngine;
using CursorLockMode = Features.Shared.Entities.CursorLockMode;

namespace Features.UI.InterfaceAdapters
{
    public sealed class CursorManager : ICursorController
    {
        public void Apply(bool isVisible, CursorLockMode lockMode)
        {
            Cursor.visible = isVisible;
            Cursor.lockState = lockMode == CursorLockMode.Locked
                ? UnityEngine.CursorLockMode.Locked
                : UnityEngine.CursorLockMode.None;
        }
    }
}
