using Features.PhysicsInteraction.Application;
using Features.Player.Insrastructure;
using Features.Player.Insrastructure.Interfaces;
using Zenject;

namespace Features.Player.Applications
{
    public class PlayerGameStartedUseCase
    {
        [Inject] private Domain.Player _player;
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