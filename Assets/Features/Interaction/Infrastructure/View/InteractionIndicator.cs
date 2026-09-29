using Features.Interaction.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Interaction.Infrastructure.View
{
    public sealed class InteractionIndicator : MonoBehaviour, IInteractionIndicator, IPanel
    {
        [SerializeField] private GameObject _indication;
        [SerializeField] private Image _focusedImage;
        [SerializeField] private Image _grabbedImage;

        private bool _active;
        private bool _isFocusedImage;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.All;

        public void InitPanel()
        {
            _active = true;
            HideIndication();
        }

        public void OnEnterPanel() => gameObject.SetActive(true);
        public void OnExitPanel() => gameObject.SetActive(false);

        public void UpdateIndicationPosition(Vector3 screenPosition)
        {
            _indication.transform.position = screenPosition;
        }

        public void ShowFocusedIndication()
        {
            if (_isFocusedImage) return;
            _isFocusedImage = true;
            _focusedImage.gameObject.SetActive(true);
            _grabbedImage.gameObject.SetActive(false);
        }

        public void ShowGrabbedIndication()
        {
            if (!_isFocusedImage) return;
            _isFocusedImage = false;
            _focusedImage.gameObject.SetActive(false);
            _grabbedImage.gameObject.SetActive(true);
        }

        public void ShowIndication()
        {
            if (_active) return;
            _active = true;
            _indication.gameObject.SetActive(true);
        }

        public void HideIndication()
        {
            if (!_active) return;
            _active = false;
            _indication.gameObject.SetActive(false);
        }
    }
}
