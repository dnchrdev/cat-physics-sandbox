using Feature.PlayerFeature;
using Feature.Storage;
using Feature.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.Tutorial
{
    public class TutorialPresenter : MonoBehaviour, IPanel, IDisposable
    {
        [SerializeField] private ImageButton _startGameButton;

        public List<UIPanelTag> PanelTags => Tags;
        private readonly List<UIPanelTag> Tags = new() { UIPanelTag.TutorialCompleted };

        private UIPanelsManager _panelManager;
        private CompleteTutorialUseCase _completeTutorialUseCase;

        [Inject]
        private void Construct(
        UIPanelsManager gameplayPanelCollection,
        PlayerProgress playerProgress,
        CompleteTutorialUseCase completeTutorialUseCase
        )
        {
            _panelManager = gameplayPanelCollection;
            _completeTutorialUseCase = completeTutorialUseCase;
            _panelManager.AddPanel(this);
        }

        public void Dispose()
        {
            _panelManager.RemovePanel(this);
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _startGameButton.Click += GetStartGame;
        }

        private void GetStartGame()
        {
            Debug.Log("CLICK");
            _completeTutorialUseCase.StartGame();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _startGameButton.Click -= _completeTutorialUseCase.StartGame;
        }
    }
}