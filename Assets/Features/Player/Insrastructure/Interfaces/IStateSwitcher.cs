namespace Feature.PlayerFeature
{
    public interface IStateSwitcher
    {
        public ICharacterState LastState { get; }
        public void Switch<T>() where T : ICharacterState;
    }
}