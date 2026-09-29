using Features.Interaction.Entities;
using UnityEngine;

namespace Features.Interaction.UseCases.Ports
{
    public interface IInteractionDetection
    {
        FocusHit DetectFocus();
        Vector3 GetAimPoint();
        Vector3 GetAimPoint(InteractableEntity ignored);
    }

    public readonly struct FocusHit
    {
        public readonly InteractableEntity Target;
        public readonly Vector3 Point;

        public FocusHit(InteractableEntity target, Vector3 point)
        {
            Target = target;
            Point = point;
        }

        public bool HasTarget => Target != null;
        public static FocusHit None => default;
    }
}
