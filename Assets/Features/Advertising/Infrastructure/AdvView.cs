using System.Collections.Generic;
using Feature.UI;
using TMPro;
using UnityEngine;

namespace Feature.Advertising
{
    public class AdvView : MonoBehaviour, IAdvView, IPanel
    {
        [SerializeField] private GameObject _interstitialPanel;
        [SerializeField] private TMP_Text _timerText;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
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
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void ShowInterstitialPanel(bool isActive)
        {
            _interstitialPanel.SetActive(isActive);
        }

        public void UpdateTimer(int seconds)
        {
            _timerText.SetText("{0}", seconds);
        }
    }
}