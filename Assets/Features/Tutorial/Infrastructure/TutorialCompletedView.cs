using System;
using Features.Tutorial.Adapter.Interfaces;
using Features.UI.Infrastructure.Data;
using Features.UI.Presentation.Buttons;
using Features.UI.Presentation.UIPanelManager.Interfaces;
using UnityEngine;

namespace Features.Tutorial.Infrastructure
{
    public class TutorialCompletedView : MonoBehaviour, ITutorialCompletedView, IPanel
    {
        public event Action StartGameRequestedEvent;

        public PanelMode[] PanelModes => new[] { PanelMode.TutorialCompleted };
        public PanelInput PanelInput => PanelInput.All;

        [SerializeField] private ImageButton _startGameButton;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _startGameButton.Click += OnStartGameButtonClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _startGameButton.Click -= OnStartGameButtonClicked;
        }

        public void Tick(float dt)
        {
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        private void OnStartGameButtonClicked() => StartGameRequestedEvent?.Invoke();
    }
}