namespace Features.Shared.Entities.Ports
{
    public interface IEntity
    {
        bool IsAlive { get; }
        TeamType Team { get; }
    }
}
