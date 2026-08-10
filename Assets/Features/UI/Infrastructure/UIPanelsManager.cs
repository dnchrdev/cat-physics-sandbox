using Feature.Core;
using System.Collections.Generic;
using Feature.Storage;
using UnityEngine;
using Zenject;

namespace Feature.UI
{
    public class UIPanelsManager : ITickable, IInitializable
    {
        [Inject] private readonly PanelsConfig _gameplayPanelConfig;
        [Inject] private readonly CursorManager _cursorManager;
        [Inject] private readonly IReadOnlyControlSettings _controlSettings;
        
        private Dictionary<PanelMode, List<IPanel>> _panels = new();
        private List<IPanel> _openedPanels = new();
        private PanelMode _currentPanelMode;

        private bool _init;
        private float _checkPanelStateTimer;
        
        public void Initialize()
        {
            _init = false;
            _currentPanelMode = default;
        }

        public Result RegisterPanel(IPanel panel)
        {
            var tags = panel.PanelModes;
            foreach (var tag in tags)
            {
                if (_panels.ContainsKey(tag))
                {
                    if (!_panels[tag].Contains(panel))
                        _panels[tag].Add(panel);
                    else
                        return Result.Failure($"Trying to add duplicate");
                }
                else
                {
                    _panels.Add(tag, new List<IPanel> { panel });
                }
            }

            panel?.InitPanel();

            return Result.Success();
        }

        public void ClearAllPanels()
        {
            foreach (var kvp in _panels)
                foreach (var panel in kvp.Value)
                    panel.OnExitPanel();
        
            _panels = new();
            _openedPanels = new();
            _init = false;
        }

        public void RemovePanel(IPanel panel)
        {
            var tags = panel.PanelModes;
            foreach (var tag in tags)
            {
                if (!_panels.ContainsKey(tag)) continue;

                bool contains = _panels[tag].Contains(panel);
                bool removed = _panels[tag].Remove(panel);
            }

            int totalPanels = 0;
            foreach (var kvp in _panels)
                totalPanels += kvp.Value.Count;

            if (totalPanels == 0) _init = false;
        }

        public void OpenPanel(PanelMode panelMode)
        {
            if (_currentPanelMode == panelMode && _init == true) return;
            
            OpenPanelContinue(panelMode);
        }

        private void OpenPanelContinue(PanelMode panelMode)
        {
            if (_panels.TryGetValue(_currentPanelMode, out var exitPanels))
                foreach (var panel in exitPanels)
                {
                    panel.OnExitPanel();
                }

            _openedPanels = new List<IPanel>();

            bool panelEntered = false;

            if (_panels.TryGetValue(panelMode, out var enterPanels))
                foreach (var panel in enterPanels)
                {
                    if(_controlSettings.IsMobile)
                        if(panel.PanelInput == PanelInput.PC) continue; 
                        
                    if(_controlSettings.IsMobile == false)
                        if(panel.PanelInput == PanelInput.Mobile) continue; 
                    
                    panel.OnEnterPanel();
                    panelEntered = true;
                    _openedPanels.Add(panel);
                }

            if (panelEntered) _init = true;
            _currentPanelMode = panelMode;

            _cursorManager.ApplyState(_gameplayPanelConfig.GetPanelConfig(panelMode));
        }

        public void Tick()
        {
            _checkPanelStateTimer -= Time.deltaTime;
            if (_checkPanelStateTimer < 0 && _init)
            {
                _cursorManager.ApplyState(_gameplayPanelConfig.GetPanelConfig(_currentPanelMode));
                _checkPanelStateTimer = 1f;
            }
        }
    }
}
