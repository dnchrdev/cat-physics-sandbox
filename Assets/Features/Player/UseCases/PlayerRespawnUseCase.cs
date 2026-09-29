using Features.Player.UseCases.Ports;

namespace Features.Player.UseCases
{
    public class PlayerRespawnUseCase
    {
        private readonly Entities.Player _player;
        private readonly ICharacterMotor _motor;
        private readonly ISpawnPoint _spawnPoint;
        private readonly IInteractableReset _interactableReset;

        public PlayerRespawnUseCase(
            Entities.Player player,
            ICharacterMotor motor,
            ISpawnPoint spawnPoint,
            IInteractableReset interactableReset)
        {
            _player = player;
            _motor = motor;
            _spawnPoint = spawnPoint;
            _interactableReset = interactableReset;
        }
        
        public void RespawnReset()
        {
            MoveToSpawnPoint();
            _interactableReset.ResetItems();
            _player.Respawn();
        }
        
        public void RespawnContinue()
        {
            MoveToSpawnPoint();
            _player.Continue();
        }

        private void MoveToSpawnPoint() => _motor.Teleport(_spawnPoint.Position, _spawnPoint.Rotation);
    }
}
