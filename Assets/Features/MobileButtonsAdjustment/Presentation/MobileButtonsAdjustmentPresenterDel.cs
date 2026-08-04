using Feature.Storage;
using Feature.UI;
using System;
using UnityEngine;
using Zenject;

namespace Feature.MobileButtonsAdjustment
{
    public class MobileButtonsAdjustmentPresenterDel
    {
        //private IReadOnlyControlSettings _controlSettings;
        //private MobileAdjustmentPresenter _mobileAdjustmentPanel;
        //private UIPanelsManager _panelManager;
        //private MobileButtonsAdjustmentEventBus _eventBus;
        //private MobileControlsSettings _mobileControlsSettings;

        //[Inject]
        //public MobileButtonsAdjustmentPresenter(
        //    IReadOnlyControlSettings controlSettings,
        //    MobileAdjustmentPresenter mobileAdjustmentPanel,
        //    UIPanelsManager gameplayPanelCollection,
        //    MobileButtonsAdjustmentEventBus eventBus,
        //    MobileControlsSettings mobileControlsSettings
        //    )
        //{
        //    _controlSettings = controlSettings;
        //    _mobileAdjustmentPanel = mobileAdjustmentPanel;
        //    _panelManager = gameplayPanelCollection;
        //    _eventBus = eventBus;
        //    _mobileControlsSettings = mobileControlsSettings;

        //    if (_controlSettings.IsMobile)
        //    {
        //        _panelManager.AddPanel(_mobileAdjustmentPanel);

        //        foreach (var btn in _mobileAdjustmentPanel.AdjustableButtons)
        //        {
        //            SetDefaultPosition(btn.Type, btn.Draggablebutton.GetAnchoredPosition());
        //        }
        //    }

        //    _mobileAdjustmentPanel.Init();
        //}

        //public void Initialize()
        //{
        //    if (_controlSettings.IsMobile)
        //    {
        //        _eventBus.UpdateVisibleButtons += _mobileAdjustmentPanel.UpdateVisibleButtons;

        //        _mobileAdjustmentPanel.CloseAdjustmentEvent += OpenGameplayPanel;
        //        _mobileAdjustmentPanel.ButtonDraggedEvent += SetAnchoredPosition;
        //        _mobileAdjustmentPanel.ResetAdjustmentEvent += HandleAdjustmentReset;

        //    }
        //}

        //private void HandleAdjustmentReset()
        //{
        //    _mobileControlsSettings.ResetToDefaults();
        //    Reseted();
        //}

        //private void Reseted()
        //{
        //    foreach (var draggableButton in _mobileAdjustmentPanel.AdjustableButtons)
        //    {
        //        if(_mobileControlsSettings.GetIsInitialized(draggableButton.Type))
        //            SetAnchoredPosition(
        //                draggableButton.Type, 
        //                draggableButton.Draggablebutton.GetRectTransform(), 
        //                _mobileControlsSettings.GetAnchoredPosition(draggableButton.Type
        //                ));
        //    }
        //}

        //private void OpenGameplayPanel()
        //{
        //    _panelManager.OpenPanel(UIPanelTag.Gameplay);
        //}

        //public void Dispose()
        //{
        //    if (_controlSettings.IsMobile)
        //    {
        //        _eventBus.UpdateVisibleButtons -= _mobileAdjustmentPanel.UpdateVisibleButtons;

        //            _panelManager.RemovePanel(_mobileAdjustmentPanel);
        //        _mobileAdjustmentPanel.CloseAdjustmentEvent -= OpenGameplayPanel;
        //        _mobileAdjustmentPanel.ButtonDraggedEvent -= SetAnchoredPosition;

        //    }
        //}

        //private void SetAnchoredPosition(AdjustableButtonType type, RectTransform element, Vector2 position)
        //{
        //    element.anchoredPosition = position;
        //    _mobileControlsSettings.SetAnchoredPosition(type, position);
        //}

        //private void SetDefaultPosition(AdjustableButtonType type, Vector2 position)
        //{
        //    if(_mobileControlsSettings.GetIsInitialized(type) == false)
        //    {
        //        _mobileControlsSettings.SetDefaultAnchoredPosition(type, position);
        //    }
        //}
    }
}