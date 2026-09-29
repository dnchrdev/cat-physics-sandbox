using Features.Interaction.Entities;
using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.UseCases
{
    public sealed class GrabUseCase
    {
        private readonly IHand _hand;
        private readonly IReadOnlyCameraTransform _camera;
        private readonly IInteractionDetection _detection;
        private readonly IInteractionConfig _config;
        private readonly IInteractionOutputBoundary _output;

        private InteractableEntity _held;
        private float _heldTime;

        public bool IsHolding => _held != null;

        public GrabUseCase(
            IHand hand,
            IReadOnlyCameraTransform camera,
            IInteractionDetection detection,
            IInteractionConfig config,
            IInteractionOutputBoundary output)
        {
            _hand = hand;
            _camera = camera;
            _detection = detection;
            _config = config;
            _output = output;
        }

        public bool TryGrab(InteractableEntity target, Vector3 grabPoint, IEntity owner)
        {
            if (target == null || owner == null) return false;
            if (IsHolding) Release(forced: true);
            if (!target.TryGrab(owner, grabPoint)) return false;

            _held = target;
            _heldTime = 0f;
            return true;
        }

        public void Tick(float deltaTime)
        {
            var grabPoint = _held.GetGrabPoint(_hand.Position, _camera.Position, _camera.Forward);
            _held.MoveGrabAnchor(grabPoint);

            if (_held.ShowsGrabPoint)
                _output.MoveIndication(grabPoint);

            _heldTime += deltaTime;

            var holdDistance = _held.GetHoldDistance(_camera.Position, _config.MinDistanceBetweenPlayerAndInteractableMesh);
            _hand.SetHoldAnchor(_held.HandAnchorDirection, holdDistance);
        }

        public bool ShouldForceRelease() =>
            !_held.IsGrabbed ||
            Vector3.Distance(_held.Position, _hand.Position) > _config.ForceGrabbedReleaseDistance;

        public void Throw()
        {
            if (_held == null) return;

            var aimPoint = _detection.GetAimPoint(ignored: _held);
            _held.Throw(aimPoint, _config.GrabbedThrowPower);
            Forget();
        }

        public void Release(bool forced)
        {
            if (_held == null) return;
            if (!forced && _heldTime < _config.ReleaseCooldown) return;

            _held.Release();
            Forget();
        }

        private void Forget()
        {
            _held = null;
            _output.CloseHints();
        }
    }
}
