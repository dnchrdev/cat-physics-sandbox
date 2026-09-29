namespace Features.Enemy.UseCases.Ports
{
    public interface IRagdoll
    {
        bool IsAnyBoneGrabbed { get; }
        void Enable();
        void Disable();
    }
}
