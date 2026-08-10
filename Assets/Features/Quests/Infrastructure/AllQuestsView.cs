using System;
using Feature.UI;
using System.Collections;
using UnityEngine;

namespace Feature.Quests
{
    public class AllQuestsView : MonoBehaviour, IAllQuestsView, IPanel
    {
        public event Action CloseRequestedEvent;
        public event Action PanelEnteredEvent;
        public event Action PanelExitedEvent;

        public PanelMode[] PanelModes => new[] { PanelMode.AllQuests };
        public PanelInput PanelInput => PanelInput.All;
        
        [SerializeField] private Transform _showedContent;
        [SerializeField] private Transform _hiddenContent;
        [SerializeField] private ImageButton _closeButton;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _closeButton.Click += OnCloseButtonClicked;
            PanelEnteredEvent?.Invoke();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _closeButton.Click -= OnCloseButtonClicked;
            PanelExitedEvent?.Invoke();
        }

        public void Tick(float dt)
        {
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public Transform GetShowedContentParent() => _showedContent;
        public Transform GetHiddenContentParent() => _hiddenContent;

        private void OnCloseButtonClicked() => CloseRequestedEvent?.Invoke();
    }
}