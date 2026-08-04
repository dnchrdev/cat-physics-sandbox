using Feature.PhysicsInteraction;
using Feature.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    //public class PCInteractionTipShower : MonoBehaviour, IPanelView, IInteractionTipShower
    //{
    //    [field: SerializeField] public UIPanelTag PanelTag { get; private set; }

    //    [SerializeField]
    //    private List<InteractionAndTips> _interactionAndTip = new();

    //    [SerializeField] private GameObject _afterGrabTipObj;

    //    private void OnValidate()
    //    {
    //        if (PanelTag == UIPanelTag.Init)
    //        {
    //            Debug.LogError($"{gameObject.name} - PanelTag is not valid");
    //        }
    //    }
    //    private void Awake()
    //    {
    //        CloseAllTips();
    //    }

    //    public void OnEnter()
    //    {
    //        gameObject.SetActive(true);
    //    }

    //    public void OnExit()
    //    {
    //        gameObject.SetActive(false);
    //    }

    //    public void Tick(float dt)
    //    {

    //    }
    //    public void CloseAllTips()
    //    {
    //        foreach (var tip in _interactionAndTip)
    //        {
    //            tip.TipObj.SetActive(false);
    //        }

    //        _afterGrabTipObj.SetActive(false);
    //    }

    //    public void ShowAfterGrabTip()
    //    {
    //        CloseAllTips();

    //        _afterGrabTipObj.SetActive(true);
    //    }

    //    public void ShowTip(InteractionType type)
    //    {
    //        CloseAllTips();

    //        //Debug.Log("TYPE = " + type);

    //        foreach (var tip in _interactionAndTip)
    //        {
    //            if (tip.InteractionType == type)
    //            {
    //                tip.TipObj.SetActive(true);
    //                break;
    //            }
    //        }
    //    }
    //}
}