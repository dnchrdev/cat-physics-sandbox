using Features.PhysicsInteraction.Application;
using Features.Player.Insrastructure;
using Features.Player.Insrastructure.Interfaces;
using Zenject;

namespace Features.Player.Applications
{
    public class PlayerRespawnUseCase
    {
        [Inject] private readonly Domain.Player _player;
        [Inject] private readonly ICharacterMotorReset _characterMotor;
        [Inject] private readonly PlayerRig _playerRig;
        [Inject] private readonly InteractableResetService _interactableResetService;

        public void RespawnReset()
        {
            _characterMotor.SetPosition(_playerRig.GameStartTransform.position);
            _characterMotor.SetRotation(_playerRig.GameStartTransform.rotation);
            _interactableResetService.ResetItems();
            _player.Respawn();
        }

        public void RespawnContinue()
        {
            _characterMotor.SetPosition(_playerRig.GameStartTransform.position);
            _characterMotor.SetRotation(_playerRig.GameStartTransform.rotation);
            _player.Continue();
        }
    }
}