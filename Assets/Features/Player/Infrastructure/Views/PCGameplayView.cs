using System;
using Features.Player.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Shared.UseCases.Input;
using UnityEngine;
using Zenject;

namespace Features.Player.Infrastructure.Views
{
    public class PCGameplayView : MonoBehaviour, IPCGameplayView, IPanel
    {
        public event Action SettingsOpenedEvent;

        private IUIPanelInput _input;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.PC;

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

        private void OnToggleSettings() => SettingsOpenedEvent?.Invoke();
    }
}
