using System;
using Features.Quests.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using Features.UI.Infrastructure.Buttons;
using UnityEngine;

namespace Features.Quests.Infrastructure.Views
{
    public class AllQuestsView : MonoBehaviour, IAllQuestsView, IPanel
    {
        private static readonly PanelMode[] Modes = { PanelMode.AllQuests };

        public event Action CloseRequested;
        public event Action PanelEntered;
        public event Action PanelExited;

        [SerializeField] private Transform _visibleCardParent;
        [SerializeField] private Transform _hiddenCardParent;
        [SerializeField] private ImageButton _closeButton;

        public PanelMode[] PanelModes => Modes;
        public PanelInput PanelInput => PanelInput.All;
        public Transform VisibleCardParent => _visibleCardParent;
        public Transform HiddenCardParent => _hiddenCardParent;

        public void InitPanel() => gameObject.SetActive(false);

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _closeButton.Click += OnCloseClicked;
            PanelEntered?.Invoke();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _closeButton.Click -= OnCloseClicked;
            PanelExited?.Invoke();
        }

        private void OnCloseClicked() => CloseRequested?.Invoke();
    }
}
