namespace Features.Player.UseCases.Movement
{
    public enum MovementMode
    {
        Walk,
        Airborne,
        Jump,
        Dead
    }
    
    public interface IMovementMode
    {
        MovementMode Enter();
        MovementMode FixedTick(float deltaTime);
    }
}
