using Features.Camera.Entities;
using Features.Camera.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Camera.UseCases
{
    public sealed class CameraHeadbobUseCase
    {
        private readonly CameraHeadbob _headbob;
        private readonly IReadOnlyMovementState _movement;
        private readonly ICameraMover _mover;

        public CameraHeadbobUseCase(CameraHeadbob headbob, IReadOnlyMovementState movement, ICameraMover mover)
        {
            _headbob = headbob;
            _movement = movement;
            _mover = mover;
        }

        public void Tick(float dt)
        {
            var input = new HeadbobInput(
                _movement.GetVelocity().magnitude,
                _movement.IsGrounded(),
                _movement.IsSliding(),
                dt);

            _mover.SetBobOffset(_headbob.Advance(input));
        }
    }
}
