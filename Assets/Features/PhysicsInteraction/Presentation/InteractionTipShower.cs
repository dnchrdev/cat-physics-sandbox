using Feature.UI;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.PhysicsInteraction
{

    public class InteractionTipShower : MonoBehaviour, IPanel, IInteractionTipShower
    {
        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
        {
            UIPanelTag.Gameplay
        };

        [SerializeField]
        private List<InteractionAndTips> _interactionAndTip = new();

        [SerializeField] private List<GameObject> _afterGrabTipObj;


        public void InitPanel()
        {
            CloseAllTips();
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

        public void CloseAllTips()
        {
            foreach (var tip in _interactionAndTip)
            {
                foreach (var obj in tip.TipObj)
                {
                    obj.SetActive(false);
                }
            }

            foreach (var tip in _afterGrabTipObj)
            {
                tip.SetActive(false);
            }

        }

        public void ShowAfterGrabTip()
        {
            CloseAllTips();

            foreach (var tip in _afterGrabTipObj)
            {
                tip.SetActive(true);
            }
        }

        public void ShowTip(InteractionType type)
        {
            CloseAllTips();


            foreach (var tip in _interactionAndTip)
            {
                if (tip.InteractionType == type)
                {
                    foreach (var obj in tip.TipObj)
                    {
                        obj.SetActive(true);
                    }
                    break;
                }
            }
        }
    }
}
 
