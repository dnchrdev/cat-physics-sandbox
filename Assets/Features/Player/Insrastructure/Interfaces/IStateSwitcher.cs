using Features.Player.Applications.FSM;

namespace Features.Player.Insrastructure.Interfaces
{
    public interface IStateSwitcher
    {
        public ICharacterState LastState { get; }
        public void Switch<T>() where T : ICharacterState;
    }
}