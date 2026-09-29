using Features.Interaction.Entities;
using Features.Interaction.UseCases.Ports;
using Features.Player.UseCases.Ports;
using Features.Shared.Entities.Ports;

namespace Features.Interaction.UseCases
{
    public sealed class InteractionRootFollowUseCase
    {
        private const float RotationSmoothing = 100f;
        private const float PositionFollowRadius = 0.25f;
        private const float PositionMaxSpeed = 25f;

        private readonly IInteractionRoot _root;
        private readonly IReadOnlyCameraTransform _camera;
        private readonly IReadOnlyCharacterMotor _characterMotor;

        public InteractionRootFollowUseCase(
            IInteractionRoot root,
            IReadOnlyCameraTransform camera,
            IReadOnlyCharacterMotor characterMotor)
        {
            _root = root;
            _camera = camera;
            _characterMotor = characterMotor;
        }

        public void TeleportToPlayer()
        {
            _root.SetPosition(_characterMotor.GetPosition());
            _root.SetRotation(_characterMotor.GetRotation());
        }

        public void FixedTick(float deltaTime)
        {
            _root.SetAngularVelocity(InteractionRootFollowCalculator.AngularVelocity(
                _root.Rotation, _camera.Rotation, RotationSmoothing, deltaTime));

            _root.SetVelocity(InteractionRootFollowCalculator.Velocity(
                _root.Position, _characterMotor.GetPosition(), PositionFollowRadius, PositionMaxSpeed, deltaTime));
        }
    }
}
