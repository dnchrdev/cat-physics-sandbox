using System;
using Features.Interaction.Entities;
using Features.Interaction.Infrastructure.Configs;
using Features.Interaction.Infrastructure.Interactable;
using Features.Interaction.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.InterfaceAdapters.Gateways
{
    public sealed class InteractionDetectionGateway : IInteractionDetection
    {
        private const float MaxRaycastDistance = 100f;

        private readonly IReadOnlyCameraTransform _camera;
        private readonly InteractionControllerConfig _config;

        public InteractionDetectionGateway(IReadOnlyCameraTransform camera, InteractionControllerConfig config)
        {
            _camera = camera;
            _config = config;
        }

        public FocusHit DetectFocus()
        {
            var start = _camera.Position;
            var end = start + _camera.Forward * _config.FocusDistance;

            if (Physics.Linecast(start, end, out var hit, _config.InteractionMask, QueryTriggerInteraction.Collide))
                return ToFocusHit(hit.collider, hit.point);

            return DetectFocusWithSphere(start, (end - start).normalized);
        }

        public Vector3 GetAimPoint()
        {
            var start = _camera.Position;
            var direction = _camera.Forward;

            return Physics.Raycast(start, direction, out var hit, MaxRaycastDistance, _config.AimingPointMask)
                ? hit.point
                : start + direction * MaxRaycastDistance;
        }

        public Vector3 GetAimPoint(InteractableEntity ignored)
        {
            var start = _camera.Position;
            var direction = _camera.Forward;
            var fallback = start + direction * MaxRaycastDistance;

            var hits = Physics.RaycastAll(start, direction, MaxRaycastDistance, _config.AimingPointMask);
            Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

            foreach (var hit in hits)
                if (ResolveInteractable(hit.collider) != ignored)
                    return hit.point;

            return fallback;
        }

        private FocusHit DetectFocusWithSphere(Vector3 start, Vector3 direction)
        {
            if (!Physics.SphereCast(start, _config.FocusSphereCastRadius, direction, out var hit,
                    _config.FocusDistance, _config.InteractionMask, QueryTriggerInteraction.Collide))
                return FocusHit.None;

            var closestPoint = hit.collider.ClosestPoint(start + direction * hit.distance);
            return ToFocusHit(hit.collider, closestPoint);
        }

        private static FocusHit ToFocusHit(Collider collider, Vector3 point)
        {
            var target = ResolveInteractable(collider);
            return target != null ? new FocusHit(target, point) : FocusHit.None;
        }

        private static InteractableEntity ResolveInteractable(Collider collider) =>
            collider.TryGetComponent<InteractableBehaviour>(out var interactable) ? interactable.Entity : null;
    }
}
