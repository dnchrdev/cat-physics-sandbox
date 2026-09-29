using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Storage.UseCases
{
    public static class ButtonPositionsSaveFormat
    {
        private static readonly AdjustableButtonType[] Slots =
        {
            AdjustableButtonType.Hit,
            AdjustableButtonType.Grab,
            AdjustableButtonType.Throw,
            AdjustableButtonType.Release,
            AdjustableButtonType.MoveJoystick,
            AdjustableButtonType.Jump
        };

        public static void Write(IReadOnlyMobileControls settings, out float[] x, out float[] y, out bool[] isDefault)
        {
            x = new float[Slots.Length];
            y = new float[Slots.Length];
            isDefault = new bool[Slots.Length];

            for (var i = 0; i < Slots.Length; i++)
            {
                var isCustom = settings.TryGetCustomPosition(Slots[i], out var position);
                x[i] = position.x;
                y[i] = position.y;
                isDefault[i] = !isCustom;
            }
        }

        public static void Read(IMobileControlsSettings settings, float[] x, float[] y, bool[] isDefault)
        {
            settings.ResetPositions();

            if (x == null || y == null || isDefault == null) return;
            if (x.Length != Slots.Length || y.Length != Slots.Length || isDefault.Length != Slots.Length) return;

            for (var i = 0; i < Slots.Length; i++)
            {
                if (!isDefault[i])
                    settings.SetCustomPosition(Slots[i], new Vector2(x[i], y[i]));
            }
        }
    }
}
