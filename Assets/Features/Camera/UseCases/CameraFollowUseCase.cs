using Features.Camera.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Camera.UseCases
{
    public sealed class CameraFollowUseCase
    {
        private readonly IReadOnlyPlayer _player;
        private readonly ICameraMover _mover;

        public CameraFollowUseCase(IReadOnlyPlayer player, ICameraMover mover)
        {
            _player = player;
            _mover = mover;
        }

        public void Tick(float dt) => _mover.SetPosition(_player.Position);
    }
}
