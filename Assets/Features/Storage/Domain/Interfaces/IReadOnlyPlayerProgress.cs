namespace Features.Storage.Domain.Interfaces
{
    public interface IReadOnlyPlayerProgress
    {
        bool IsTutorialCompleted { get; }
    }
}