using System;
using Features.UI.Presentation.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Quests.Infrastructure
{
    public class QuestCard : MonoBehaviour
    {
        public event Action<string> QuestClicked;

        [SerializeField] private ImageButton _selectButton;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private Image _fillImage;

        private string _questName;

        private void Awake()
        {
            //if(_selectButton == null) _selectButton = gameObject.AddComponent<ImageButton>();
            _selectButton.Click += OnQuestSelected;
        }

        public void Init(string name, string description, float fillAmount)
        {
            _questName = name;
            _description.text = description;
            _fillImage.fillAmount = fillAmount;
        }

        private void OnQuestSelected()
        {
            QuestClicked?.Invoke(_questName);
        }
    }
}