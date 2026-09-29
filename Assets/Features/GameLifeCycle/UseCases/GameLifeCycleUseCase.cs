using System.Collections.Generic;
using Features.GameLifeCycle.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using ILogger = Features.Core.UseCases.Ports.ILogger;

namespace Features.GameLifeCycle.UseCases
{
    public sealed class GameLifeCycleUseCase
    {
        private readonly IEnumerable<IPanel> _allPanels;
        private readonly IUIPanelsManager _panelManager;
        private readonly IEnumerable<IGameStartHandler> _gameStartHandlers;
        private readonly ILogger _logger;

        public GameLifeCycleUseCase(
            IEnumerable<IPanel> allPanels,
            IUIPanelsManager panelManager,
            IEnumerable<IGameStartHandler> gameStartHandlers,
            ILogger logger)
        {
            _allPanels = allPanels;
            _panelManager = panelManager;
            _gameStartHandlers = gameStartHandlers;
            _logger = logger;
        }

        public void Execute()
        {
            foreach (var panel in _allPanels)
            {
                panel.InitPanel();
                var result = _panelManager.RegisterPanel(panel);
                if (result.IsSuccess == false)
                {
                    _logger.LogError(GetType(), result.Error);
                }
            }

            foreach (var handler in _gameStartHandlers)
                handler.OnGameStarted();

            _panelManager.OpenPanel(PanelMode.Gameplay);
        }
    }
}
