using System;
using Features.Input.Infrastructure.Interfaces;
using Features.Player.Adapter.Interfaces;
using Features.UI.Infrastructure.Data;
using Features.UI.Presentation.UIPanelManager.Interfaces;
using UnityEngine;
using Zenject;

namespace Features.Player.Insrastructure
{
    public class PCGameplayView : MonoBehaviour, IPCGameplayView, IPanel
    {
        public event Action SettingsOpenedEvent;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.PC;

        private IUIPanelInput _input;

        [Inject]
        private void Construct(IUIPanelInput input)
        {
            _input = input;
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _input.ToggleSettingsEvent += OnToggleSettings;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _input.ToggleSettingsEvent -= OnToggleSettings;
        }

        private void OnToggleSettings()
        {
            SettingsOpenedEvent?.Invoke();
        }
    }
}