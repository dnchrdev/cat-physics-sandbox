using Features.Interaction.InterfaceAdapters.Ports;
using Features.Shared.Entities.Ports;
using Features.Shared.Infrastructure;
using UnityEngine;

namespace Features.Interaction.Infrastructure.View
{
    public sealed class ScreenProjection : IScreenProjection
    {
        private const float FacingAngle = 90f;

        private readonly IReadOnlyCameraTransform _cameraTransform;
        private readonly IReadOnlyCameraPhysical _cameraPhysical;

        public ScreenProjection(IReadOnlyCameraTransform cameraTransform, IReadOnlyCameraPhysical cameraPhysical)
        {
            _cameraTransform = cameraTransform;
            _cameraPhysical = cameraPhysical;
        }

        public bool IsFacingPoint(Vector3 worldPosition) =>
            Vector3.Angle(_cameraTransform.Forward, worldPosition - _cameraTransform.Position) < FacingAngle;

        public Vector3 WorldToScreenPoint(Vector3 worldPosition) =>
            _cameraPhysical.Camera.WorldToScreenPoint(worldPosition);
    }
}
