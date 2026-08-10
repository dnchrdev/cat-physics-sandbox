using System;
using System.Collections.Generic;
using Feature.Input;
using Feature.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Feature.Quests
{
    public class PCCurrentQuestView : MonoBehaviour, ICurrentQuestView, IPanel
    {
        public event Action HintsRequestedEvent;
        public event Action AllQuestsRequestedEvent;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.PC;

        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _fillImage;

        [Inject] private readonly IUIPanelInput _input;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _input.AllQuestsEvent += OnAllQuestsButtonClicked;
            _input.QuestHintsEvent += OnHintsButtonClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _input.AllQuestsEvent -= OnAllQuestsButtonClicked;
            _input.QuestHintsEvent -= OnHintsButtonClicked;
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