using Features.GameLifeCycle.UseCases.Ports;
using Features.Player.UseCases.Ports;

namespace Features.Player.UseCases
{
    public class GameStartedUseCase : IGameStartHandler
    {
        private readonly ICharacterMotor _motor;
        private readonly ISpawnPoint _spawnPoint;

        public GameStartedUseCase(ICharacterMotor motor, ISpawnPoint spawnPoint)
        {
            _motor = motor;
            _spawnPoint = spawnPoint;
        }

        public void OnGameStarted() => _motor.Teleport(_spawnPoint.Position, _spawnPoint.Rotation);
    }
}
