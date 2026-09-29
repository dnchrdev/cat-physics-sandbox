using UnityEngine;

namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyMobileControls
    {
        int JoystickRadius { get; }
        int MaxJoystickRadius { get; }
        int MinJoystickRadius { get; }
        bool IsDynamicJoystick { get; }
        bool IsFollowJoystick { get; }

        bool HasCustomPositions { get; }

        bool TryGetCustomPosition(AdjustableButtonType button, out Vector2 position);
    }
}
