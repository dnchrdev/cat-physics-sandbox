using System;
using Feature.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.Quests
{
    public class MobileCurrentQuestView : MonoBehaviour, ICurrentQuestView, IPanel
    {
        public event Action HintsRequestedEvent;
        public event Action AllQuestsRequestedEvent;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.Mobile;

        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _fillImage;
        [SerializeField] private ImageButton _hintsButton;
        [SerializeField] private ImageButton _allQuestsButton;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _hintsButton.Click += OnHintsButtonClicked;
            _allQuestsButton.Click += OnAllQuestsButtonClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _hintsButton.Click -= OnHintsButtonClicked;
            _allQuestsButton.Click -= OnAllQuestsButtonClicked;
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetDescription(string description)
        {
            _description.text = description;
        }

        public void SetProgress(float ratio)
        {
            _fillImage.fillAmount = ratio;
        }

        private void OnHintsButtonClicked() => HintsRequestedEvent?.Invoke();
        private void OnAllQuestsButtonClicked() => AllQuestsRequestedEvent?.Invoke();
    }
}