using Features.Camera.Entities;
using Features.Camera.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Camera.UseCases
{
    public sealed class CameraLeanUseCase
    {
        private readonly CameraLean _lean;
        private readonly IReadOnlyMovementState _movement;
        private readonly ICameraMover _mover;

        public CameraLeanUseCase(CameraLean lean, IReadOnlyMovementState movement, ICameraMover mover)
        {
            _lean = lean;
            _movement = movement;
            _mover = mover;
        }

        public void Tick(float dt)
        {
            var input = new LeanInput(
                _movement.GetAcceleration(),
                _movement.IsSliding(),
                _mover.RotationRootForward,
                _mover.RotationRootRight,
                dt);

            _mover.SetLeanRotation(_lean.Advance(input));
        }
    }
}
