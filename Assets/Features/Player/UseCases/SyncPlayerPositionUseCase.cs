using Features.Player.UseCases.Ports;

namespace Features.Player.UseCases
{
    public class SyncPlayerPositionUseCase
    {
        private readonly Entities.Player _player;
        private readonly IReadOnlyCharacterMotor _motor;

        public SyncPlayerPositionUseCase(Entities.Player player, IReadOnlyCharacterMotor motor)
        {
            _player = player;
            _motor = motor;
        }

        public void Execute()
        {
            _player.UpdatePosition(_motor.GetPosition());
        }
    }
}
