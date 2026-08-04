using System;
using System.Collections.Generic;
using Feature.Input;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.PlayerFeature
{
    public class PCGameplayPresenter : MonoBehaviour, IPanel
    {
        public Action OpenSettingsEvent;

        [Inject] private IUIPanelInput _input;

        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
        {
            UIPanelTag.Gameplay
        };

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _input.ToggleSettingsEvent += OnToggleSettingsClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _input.ToggleSettingsEvent -= OnToggleSettingsClicked;
        }

        private void OnToggleSettingsClicked()
        {
            OpenSettingsEvent?.Invoke();
        }
    }
}