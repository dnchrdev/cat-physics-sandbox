using System.Collections.Generic;
using Features.Interaction.Entities;
using Features.Interaction.InterfaceAdapters.Ports;
using Features.Shared.Entities;
using Features.Shared.UseCases;
using UnityEngine;

namespace Features.Interaction.Infrastructure.View
{
    public sealed class InteractionHintsShower : MonoBehaviour, IInteractionHintsShower, IPanel
    {
        [SerializeField] private List<InteractionAndHints> _interactionAndHint = new();
        [SerializeField] private List<GameObject> _afterGrabHintObj;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.All;

        public void InitPanel()
        {
            CloseAllHints();
            gameObject.SetActive(false);
        }

        public void OnEnterPanel() => gameObject.SetActive(true);
        public void OnExitPanel() => gameObject.SetActive(false);

        public void CloseAllHints()
        {
            foreach (var hint in _interactionAndHint)
            foreach (var obj in hint.HintObj)
                obj.SetActive(false);

            foreach (var hint in _afterGrabHintObj)
                hint.SetActive(false);
        }

        public void ShowAfterGrabHint()
        {
            CloseAllHints();
            foreach (var hint in _afterGrabHintObj)
                hint.SetActive(true);
        }

        public void ShowHint(InteractionType type)
        {
            CloseAllHints();
            foreach (var hint in _interactionAndHint)
            {
                if (hint.InteractionType != type) continue;
                foreach (var obj in hint.HintObj)
                    obj.SetActive(true);
                break;
            }
        }
    }
}
