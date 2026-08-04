using System;
using System.Collections.Generic;
using Feature.Advertising;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PlayerKnockoutPanel : MonoBehaviour, IPanel
    {
        public event Action OpenGameplay;

        [SerializeField] private ImageButton _continueButton;
        [SerializeField] private ImageButton _restartButton;

        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
        {
            UIPanelTag.Knockout
        };

        private PlayerRespawnUseCase _respawnUseCase;
        private IAdvRequestService _advRequestService;

        [Inject]
        private void Construct(PlayerRespawnUseCase respawnUseCase, IAdvRequestService advRequestService)
        {
            _respawnUseCase = respawnUseCase;
            _advRequestService = advRequestService;
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);
            _restartButton.Click += OnRestartCLicked;
            _continueButton.Click += OnContinueClicked;
        }

        private void OnContinueClicked()
        {
            _advRequestService.RewardedAdvRequest(ContinueCallback);
        }

        private void ContinueCallback()
        {
            _respawnUseCase.RespawnContinue();
            OpenGameplay?.Invoke();
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);
            _restartButton.Click -= OnRestartCLicked;
            _continueButton.Click -= OnContinueClicked;
        }

        public void OnRestartCLicked()
        {
            _respawnUseCase.RespawnReset();
            OpenGameplay?.Invoke();
        }

        public void Tick(float dt)
        {
        }
    }
}