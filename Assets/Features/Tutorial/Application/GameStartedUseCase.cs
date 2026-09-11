using System.Collections.Generic;
using Features.Player.Applications;
using Features.UI.Infrastructure;
using Features.UI.Infrastructure.Data;
using Features.UI.Presentation.UIPanelManager.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Tutorial.Application
{
    public class GameStartedUseCase: IInitializable
    {
        [Inject] private IEnumerable<IPanel> _allPanels;
        [Inject] private UIPanelsManager _panelManager;
        [Inject] private PlayerGameStartedUseCase _playerUseCase;
        
        public void Initialize()
        {
            foreach (var panel in _allPanels)
            {
                panel.InitPanel();
                _panelManager.RegisterPanel(panel);
                Debug.Log($"Panel {panel.GetType().Name} has been initialized");
            }
            
            _playerUseCase.GameStarted();
            _panelManager.OpenPanel(PanelMode.Gameplay);
        }
    }
}