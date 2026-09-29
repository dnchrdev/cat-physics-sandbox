using UnityEngine;

namespace Features.Shared.Entities.Ports
{
    public interface IMobileControlsSettings : IReadOnlyMobileControls
    {
        void SetJoystickRadius(int radius);
        void SetDynamicJoystick(bool isDynamic);
        void SetFollowJoystick(bool follow);
        void SetCustomPosition(AdjustableButtonType button, Vector2 position);

        void ResetPositions();
    }
}
