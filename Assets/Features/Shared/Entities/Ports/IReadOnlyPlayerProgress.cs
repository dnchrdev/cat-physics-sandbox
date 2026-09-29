namespace Features.Shared.Entities.Ports
{
    public interface IReadOnlyPlayerProgress
    {
        bool IsTutorialCompleted { get; }
    }
}
