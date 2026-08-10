using System;
using System.Collections.Generic;
using Feature.Input;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
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
            Debug.Log("SETTINGS TOGGLE");
            SettingsOpenedEvent?.Invoke();
        }
    }
}