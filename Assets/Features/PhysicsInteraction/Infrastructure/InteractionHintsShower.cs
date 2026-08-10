using Feature.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractionHintsShower : MonoBehaviour, IInteractionTipShower, IPanel
    {
        [SerializeField] private List<InteractionAndHints> _interactionAndHint = new();
        [SerializeField] private List<GameObject> _afterGrabhintObj;

        public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
        public PanelInput PanelInput => PanelInput.All;

        public void InitPanel()
        {
            CloseAllHints();
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

        public void CloseAllHints()
        {
            foreach (var hint in _interactionAndHint)
            {
                foreach (var obj in hint.HintObj)
                {
                    obj.SetActive(false);
                }
            }

            foreach (var hint in _afterGrabhintObj)
            {
                hint.SetActive(false);
            }

        }

        public void ShowAfterGrabHint()
        {
            CloseAllHints();

            foreach (var hint in _afterGrabhintObj)
            {
                hint.SetActive(true);
            }
        }

        public void ShowHint(InteractionType type)
        {
            CloseAllHints();
            
            foreach (var hint in _interactionAndHint)
            {
                if (hint.InteractionType == type)
                {
                    foreach (var obj in hint.HintObj)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }
            }
        }
    }
}
 
