namespace Features.Shared
{
    public enum TeamType
    {
        None, Enemy, Player
    }

    public interface IEntity
    {
        bool IsAlive { get; }   
        TeamType Team {get;}
    }
}