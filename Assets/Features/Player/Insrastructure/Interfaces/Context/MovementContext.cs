using Feature.CameraFeature;
using Feature.Input;
using Zenject;

namespace Feature.PlayerFeature
{
    public sealed class MovementContext
    {
        [Inject] public CameraRig CameraRig { get; private set; }
        [Inject] public IStateSwitcher ModuleSwitcher { get; private set; }
        [Inject] public IMovementInput MoveInput { get; private set; }
        [Inject] public IInteractionInput InteractInput { get; private set; }
        [Inject] public Player Player { get; private set; }
        [Inject] public ICharacterMotor Motor { get; private set; }
        [Inject] public IReadOnlyCharacterMotor ReadOnlyMotor { get; private set; }
        [Inject] public CharacterConfig Config { get; private set; }
        [Inject] public IReadOnlyCamera ReadOnlyCamera { get; private set; }
        [Inject] public SurfaceDetector SurfaceDetector { get; private set; }
        [Inject] public SlidePhysicsCalculator SlidePhysicsCalculator { get; private set; }
    }
}