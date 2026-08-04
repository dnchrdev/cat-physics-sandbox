namespace Feature.PlayerFeature
{
    public interface ICharacterState
    {
        public void Enter();
        public void Exit();
        public void Tick(float dt);
        public void FixedTick(float dt);
    }
}