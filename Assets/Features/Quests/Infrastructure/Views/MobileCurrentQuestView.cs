using System;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.UI.Infrastructure.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quests.Infrastructure.Views
{
    public class MobileCurrentQuestView : MonoBehaviour, ICurrentQuestView, IPanel
    {
        public event Action HintsRequested;
        public event Action AllQuestsRequested;
        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.Mobile;

        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _fillImage;
        [SerializeField] private ImageButton _hintsButton;
        [SerializeField] private ImageButton _allQuestsButton;

        public void InitPanel() => gameObject.SetActive(false);

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _hintsButton.Click += OnHintsClicked;
            _allQuestsButton.Click += OnAllQuestsClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _hintsButton.Click -= OnHintsClicked;
            _allQuestsButton.Click -= OnAllQuestsClicked;
        }

        public void SetDescription(string description) => _description.text = description;
        public void SetProgress(float progress) => _fillImage.fillAmount = progress;
        private void OnHintsClicked() => HintsRequested?.Invoke();
        private void OnAllQuestsClicked() => AllQuestsRequested?.Invoke();
    }
}
