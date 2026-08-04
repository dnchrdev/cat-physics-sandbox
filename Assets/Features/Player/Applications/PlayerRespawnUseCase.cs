using Feature.PhysicsInteraction;
using Feature.Quests;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerRespawnUseCase
    {
        [Inject] private readonly Player _player;
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