using Features.Camera.Entities;
using Features.Camera.UseCases.Ports;
using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Camera.UseCases
{
    public sealed class CameraRotationUseCase
    {
        private readonly CameraRotation _rotation;
        private readonly IReadOnlyControlSettings _controlSettings;
        private readonly ICameraMover _mover;

        public CameraRotationUseCase(CameraRotation rotation, IReadOnlyControlSettings controlSettings, ICameraMover mover)
        {
            _rotation = rotation;
            _controlSettings = controlSettings;
            _mover = mover;
        }

        public void Tick(Vector2 lookDelta, float dt)
        {
            var rotation = _rotation.Advance(lookDelta, _controlSettings.LookSensitivity, _mover.RotationRootRotation, dt);
            _mover.SetRotation(rotation);
        }
    }
}
