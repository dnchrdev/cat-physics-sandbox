using System;
using System.Collections.Generic;
using Feature.UI;
using TMPro;
using UnityEngine;
using Zenject;

namespace Feature.Advertising
{
    public class AdvPresenter : MonoBehaviour, IPanel, IInitializable, IDisposable
    {
        [SerializeField] private GameObject _interstitialPanel;
        [SerializeField] private TMP_Text _timerText;

        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new() { UIPanelTag.Gameplay };

        private UIPanelsManager _panelsManager;

        [Inject]
        private void Construct(UIPanelsManager panelsManager)
        {
            _panelsManager = panelsManager;

            _panelsManager.AddPanel(this);
        }

        public void Dispose()
        {
            _panelsManager.RemovePanel(this);
        }

        public void Initialize()
        {
        }

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

        public void ShowInterstitialPanel(bool active)
        {
            _interstitialPanel.SetActive(active);
        }

        public void UpdateTimer(int timer)
        {
            _timerText.SetText("{0}", timer);
        }
    }
}