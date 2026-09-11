using Features.Camera.Infrastructure;
using Features.Camera.Infrastructure.Interfaces;
using Features.Input.Infrastructure.Interfaces;
using Features.Player.Applications;
using Features.Player.Insrastructure.Configs;
using Features.Player.Insrastructure.Surface;
using Zenject;

namespace Features.Player.Insrastructure.Interfaces.Context
{
    public sealed class MovementContext
    {
        [Inject] public CameraRig CameraRig { get; private set; }
        [Inject] public IStateSwitcher ModuleSwitcher { get; private set; }
        [Inject] public IMovementInput MoveInput { get; private set; }
        [Inject] public IInteractionInput InteractInput { get; private set; }
        [Inject] public Domain.Player Player { get; private set; }
        [Inject] public ICharacterMotor Motor { get; private set; }
        [Inject] public IReadOnlyCharacterMotor ReadOnlyMotor { get; private set; }
        [Inject] public PlayerCharacterConfig Config { get; private set; }
        [Inject] public IReadOnlyCamera ReadOnlyCamera { get; private set; }
        [Inject] public SurfaceDetector SurfaceDetector { get; private set; }
        [Inject] public SlidePhysicsCalculator SlidePhysicsCalculator { get; private set; }
    }
}