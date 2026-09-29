using Features.GameLifeCycle.UseCases;
using Zenject;

namespace Features.GameLifeCycle.InterfaceAdapters
{
    public sealed class GameLifeCycleController : IInitializable
    {
        private readonly GameLifeCycleUseCase _gameLifeCycle;

        public GameLifeCycleController(GameLifeCycleUseCase gameLifeCycle)
        {
            _gameLifeCycle = gameLifeCycle;
        }

        public void Initialize() => _gameLifeCycle.Execute();
    }
}
