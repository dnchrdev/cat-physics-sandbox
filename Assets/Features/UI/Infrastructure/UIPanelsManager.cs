using Feature.Core;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.UI
{
    public class UIPanelsManager : ITickable
    {
        private UIPanelConfig _gameplayPanelConfig;

        private CursorManager _cursorManager;
        Dictionary<UIPanelTag, List<IPanel>> _panels = new();
        private List<IPanel> _openedPanels = new();
        private UIPanelTag _currentPanelTag;

        private bool _init;
        private float _checkPanelStateTimer;
        
        [Inject]
        public UIPanelsManager(UIPanelConfig gameplayPanelConfig, CursorManager cursorManager)
        {
            _gameplayPanelConfig = gameplayPanelConfig;
            _currentPanelTag = default;
            _cursorManager = cursorManager;
            _init = false;
        }

        public Result AddPanel(IPanel panel)
        {
            var tags = panel.PanelTags;
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
            
            var tags = panel.PanelTags;
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

        public void OpenPanel(UIPanelTag panelTag)
        {
            if (_currentPanelTag == panelTag && _init == true) return;
            
            OpenPanelContinue(panelTag);
        }

        private void OpenPanelContinue(UIPanelTag panelTag)
        {
            if (_panels.TryGetValue(_currentPanelTag, out var exitPanels))
                foreach (var panel in exitPanels)
                {
                    panel.OnExitPanel();
                }

            _openedPanels = new List<IPanel>();

            bool panelEntered = false;

            if (_panels.TryGetValue(panelTag, out var enterPanels))
                foreach (var panel in enterPanels)
                {
                    panel.OnEnterPanel();
                    panelEntered = true;
                    _openedPanels.Add(panel);
                }

            if (panelEntered) _init = true;
            _currentPanelTag = panelTag;

            _cursorManager.ApplyState(_gameplayPanelConfig.GetPanelState(panelTag));
        }

        public void Tick()
        {
            _checkPanelStateTimer -= Time.deltaTime;
            if (_checkPanelStateTimer < 0 && _init)
            {
                _cursorManager.ApplyState(_gameplayPanelConfig.GetPanelState(_currentPanelTag));
                _checkPanelStateTimer = 1f;
            }
        }
    }
}
