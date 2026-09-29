using System.Collections.Generic;
using Features.Core.UseCases;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;
using Features.Shared.Utilities;
using Features.UI.UseCases.Ports;

namespace Features.UI.UseCases
{
    public sealed class UIPanelsManager : IUIPanelsManager
    {
        private const float PanelStateReapplyIntervalSeconds = 1f;

        private readonly IPanelsConfigProvider _panelsConfig;
        private readonly ICursorController _cursor;
        private readonly GamePauseService _pause;
        private readonly IReadOnlyControlSettings _controlSettings;

        private Dictionary<PanelMode, List<IPanel>> _panels = new();
        private PanelMode _currentPanelMode;
        private bool _hasOpenPanel;
        private float _reapplyTimer;

        public UIPanelsManager(
            IPanelsConfigProvider panelsConfig,
            ICursorController cursor,
            GamePauseService pause,
            IReadOnlyControlSettings controlSettings)
        {
            _panelsConfig = panelsConfig;
            _cursor = cursor;
            _pause = pause;
            _controlSettings = controlSettings;
        }

        public Result RegisterPanel(IPanel panel)
        {
            foreach (var mode in panel.PanelModes)
            {
                if (!_panels.TryGetValue(mode, out var panels))
                {
                    _panels.Add(mode, new List<IPanel> { panel });
                    continue;
                }

                if (panels.Contains(panel))
                    return Result.Failure("Trying to add duplicate");

                panels.Add(panel);
            }

            return Result.Success();
        }

        public void ClearAllPanels()
        {
            foreach (var panels in _panels.Values)
                foreach (var panel in panels)
                    panel.OnExitPanel();

            _panels = new Dictionary<PanelMode, List<IPanel>>();
            _hasOpenPanel = false;
        }

        public void OpenPanel(PanelMode panelMode)
        {
            if (_currentPanelMode == panelMode && _hasOpenPanel)
                return;

            if (_panels.TryGetValue(_currentPanelMode, out var exitPanels))
                foreach (var panel in exitPanels)
                    panel.OnExitPanel();

            var isAnyPanelEntered = false;
            if (_panels.TryGetValue(panelMode, out var enterPanels))
            {
                foreach (var panel in enterPanels)
                {
                    if (!IsForCurrentInput(panel))
                        continue;

                    panel.OnEnterPanel();
                    isAnyPanelEntered = true;
                }
            }

            _currentPanelMode = panelMode;

            if (!isAnyPanelEntered)
                return;

            _hasOpenPanel = true;
            ApplyPanelState(panelMode);
        }

        public void Tick(float deltaTime)
        {
            _reapplyTimer -= deltaTime;
            if (_reapplyTimer >= 0f || !_hasOpenPanel)
                return;

            ApplyPanelState(_currentPanelMode);
            _reapplyTimer = PanelStateReapplyIntervalSeconds;
        }

        private bool IsForCurrentInput(IPanel panel) =>
            panel.PanelInput == PanelInput.All ||
            panel.PanelInput == (_controlSettings.IsMobile ? PanelInput.Mobile : PanelInput.PC);

        private void ApplyPanelState(PanelMode mode)
        {
            var state = _panelsConfig.GetPanelConfig(mode);
            _cursor.Apply(state.IsCursorVisible, state.LockState);

            if (state.IsPaused)
                _pause.Pause();
            else
                _pause.Resume();
        }
    }
}
