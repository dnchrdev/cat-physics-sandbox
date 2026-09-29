using System;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.Shared.UseCases.Input;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Features.Quests.Infrastructure.Views
{
    public class PCCurrentQuestView : MonoBehaviour, ICurrentQuestView, IPanel
    {
        public event Action HintsRequested;
        public event Action AllQuestsRequested;
        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.PC;

        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _fillImage;

        private IUIPanelInput _input;

        [Inject]
        private void Construct(IUIPanelInput input)
        {
            _input = input;
        }

        public void InitPanel() => gameObject.SetActive(false);

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _input.AllQuestsEvent += OnAllQuestsClicked;
            _input.QuestHintRequestedEvent += OnHintsClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _input.AllQuestsEvent -= OnAllQuestsClicked;
            _input.QuestHintRequestedEvent -= OnHintsClicked;
        }

        public void SetDescription(string description) => _description.text = description;
        public void SetProgress(float progress) => _fillImage.fillAmount = progress;
        private void OnHintsClicked() => HintsRequested?.Invoke();
        private void OnAllQuestsClicked() => AllQuestsRequested?.Invoke();
    }
}
