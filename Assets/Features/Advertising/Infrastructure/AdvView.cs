using Features.Advertising.InterfaceAdapters.Presenters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using TMPro;
using UnityEngine;

namespace Features.Advertising.Infrastructure
{
    public class AdvView : MonoBehaviour, IAdvView, IPanel
    {
        [SerializeField] private GameObject _interstitialPanel;
        [SerializeField] private TMP_Text _timerText;

        private static readonly PanelMode[] Modes = { PanelMode.Gameplay };

        public PanelMode[] PanelModes => Modes;
        public PanelInput PanelInput => PanelInput.All;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
        }

        public void ShowTimer()
        {
            _interstitialPanel.SetActive(true);
        }

        public void HideTimer()
        {
            _interstitialPanel.SetActive(false);
        }
        
        public void UpdateTimer(int seconds)
        {
            _timerText.SetText("{0}", seconds);
        }
    }
}