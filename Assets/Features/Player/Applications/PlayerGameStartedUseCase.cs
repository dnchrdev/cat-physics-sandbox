using Feature.PhysicsInteraction;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerGameStartedUseCase
    {
        [Inject] private Player _player;
        [Inject] private ICharacterMotorReset _characterMotor;
        [Inject] private PlayerRig _playerRig;
        [Inject] private InteractableResetService _interactableResetService;

        public void GameStarted()
        {
            _characterMotor.SetPosition(_playerRig.GameStartTransform.position);
            _characterMotor.SetRotation(_playerRig.GameStartTransform.rotation);
        }
    }
}