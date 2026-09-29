using Features.Interaction.Entities;
using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.UseCases
{
    public sealed class FocusUseCase
    {
        private readonly IInteractionDetection _detection;
        private readonly IInteractionOutputBoundary _output;
        private readonly IReadOnlyCameraTransform _camera;

        public InteractableEntity Target { get; private set; }
        public Vector3 FocusPoint { get; private set; }

        public FocusUseCase(
            IInteractionDetection detection,
            IInteractionOutputBoundary output,
            IReadOnlyCameraTransform camera)
        {
            _detection = detection;
            _output = output;
            _camera = camera;
        }

        public void Tick()
        {
            var hit = _detection.DetectFocus();
            if (!hit.HasTarget)
            {
                Clear();
                return;
            }

            if (hit.Target != Target)
            {
                Clear();
                FocusOn(hit.Target);
            }

            FocusPoint = hit.Point;
            if (Target.ShowsGrabPoint)
                _output.MoveIndication(hit.Point);
        }

        public void Hit(float power, IEntity owner)
        {
            if (Target == null) return;

            var aimPoint = _detection.GetAimPoint();
            var direction = (aimPoint - _camera.Position).normalized;
            Target.HitWith(direction, aimPoint, power, owner);
        }

        public void Clear()
        {
            _output.HideIndication();
            if (Target == null) return;

            Target.Unfocus();
            Target = null;
            _output.CloseHints();
        }

        private void FocusOn(InteractableEntity target)
        {
            Target = target;
            target.Focus();
            _output.ShowHint(target.InteractionType);

            if (target.ShowsGrabPoint)
                _output.ShowIndication(IndicationType.Focus);
        }
    }
}
