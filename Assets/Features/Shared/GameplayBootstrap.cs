using System.Collections.Generic;
using Feature.PlayerFeature;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.Shared
{
    public class GameplayBootstrap: MonoBehaviour
    {
        private IEnumerable<IPanel> _allPanels;
        private  UIPanelsManager _panelManager;
        private  PlayerGameStartedUseCase _playerGameStartedUseCase;

        [Inject]
        private void Construct(IEnumerable<IPanel> allPanels, UIPanelsManager panelManager, PlayerGameStartedUseCase playerGameStartedUseCase)
        {
            _allPanels = allPanels;
            _panelManager =  panelManager;
            _playerGameStartedUseCase = playerGameStartedUseCase;
        }
        
        public void Awake()
        {
            foreach (var panel in _allPanels)
            {
                panel.InitPanel();
                _panelManager.RegisterPanel(panel);
            }
            
            _panelManager.OpenPanel(PanelMode.Gameplay);
            _playerGameStartedUseCase.GameStarted();
        }
    }
}