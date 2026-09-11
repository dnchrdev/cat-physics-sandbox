using Features.MobileButtonsAdjustment.Adapter;
using UnityEngine;

namespace Features.Storage.Domain.Interfaces
{
    public interface IReadOnlyMobileControls
    {
        public int JoystickRadius { get; }
        public int MaxJoystickRadius { get; }
        public int MinJoystickRadius { get; }
        public bool IsDynamicJoystick { get; }
        public bool IsFollowJoystick { get; }

        bool GetIsInitialized(AdjustableButtonType adjustableButton);
        Vector2 GetAnchoredPosition(AdjustableButtonType adjustableButton);

        //void SetAnchoredPosition(AdjustableButtonType adjustableButton, Vector2 position);
        //void SetDefaultAnchoredPosition(AdjustableButtonType adjustableButton, Vector2 position);
    }
}