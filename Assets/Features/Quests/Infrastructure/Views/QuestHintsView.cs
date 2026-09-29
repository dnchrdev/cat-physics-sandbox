using Features.Shared.Entities;
using Features.Shared.UseCases;
using UnityEngine;

namespace Features.Quests.Infrastructure.Views
{
    public class QuestHintsView : MonoBehaviour, IPanel
    {
        private static readonly PanelMode[] Modes = { PanelMode.Gameplay };

        [SerializeField] private Transform _visibleHintParent;
        [SerializeField] private Transform _hiddenHintParent;

        public PanelMode[] PanelModes => Modes;
        public PanelInput PanelInput => PanelInput.All;
        public Transform VisibleHintParent => _visibleHintParent;
        public Transform HiddenHintParent => _hiddenHintParent;

        public void InitPanel() => gameObject.SetActive(false);
        public void OnEnterPanel() => gameObject.SetActive(true);
        public void OnExitPanel() => gameObject.SetActive(false);
    }
}
