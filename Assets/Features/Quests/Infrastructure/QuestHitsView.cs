using System.Collections;
using Feature.UI;
using UnityEngine;

namespace Feature.Quests
{
    public class QuestHintsView : MonoBehaviour, IQuestHintsView, IPanel
    {
        [SerializeField] private Transform _visibleTipParent;
        [SerializeField] private Transform _hiddenTipParent;
        
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

        public void Tick(float dt)
        {
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public Transform GetVisibleTipParent() => _visibleTipParent;
        public Transform GetHiddenTipParent() => _hiddenTipParent;
    }
}