using System;
using System.Collections.Generic;
using Feature.Storage;
using Feature.UI;
using UnityEngine;
using Zenject;

namespace Feature.MobileButtonsAdjustment
{
    [Serializable]
    public enum AdjustableButtonType
    {
        Hit,
        Grab,
        Throw,
        Release,
        MoveJoystick,
        Jump
    }

    [Serializable]
    public struct AdjustableButtonsMap
    {
        public AdjustableButtonType Type;
        public GameObject Button;
        public DraggableUIElement Draggablebutton;
    }

    public class MobileButtonsAdjustmentPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly IStorageDataService _storageDataService;
        [Inject] private readonly MobileControlsSettings _mobileControlsSettings;
        [Inject] private readonly IReadOnlyControlSettings _controlSettings;
        [Inject] private readonly UIPanelsManager _panelManager;
        [Inject] private readonly MobileAdjustmentEventBus _eventBus;
        [Inject] private readonly IMobileButtonsAdjustmentView _view;
        
        private Dictionary<AdjustableButtonType, bool> _visibility = new();
        private bool _changed;
        
        public void Initialize()
        {
            foreach (var btn in _view.AdjustableButtons)
            {
                SetDefaultPosition(btn.Type, btn.Draggablebutton.GetAnchoredPosition());
            }

            _view.ApplyButtonPositions(_mobileControlsSettings);

            SubscribeToView();
            _eventBus.OnMobileAdjustmentButtonShow += HandleAdjustmentButtonShow;
        }

        public void Dispose()
        {
            UnsubscribeFromView();
            _eventBus.OnMobileAdjustmentButtonShow -= HandleAdjustmentButtonShow;
        }

        private void SubscribeToView()
        {
            _view.CloseRequestedEvent += HandleClose;
            _view.ResetRequestedEvent += HandleReset;
            _view.ButtonDraggedEvent += HandleButtonDragged;
        }

        private void UnsubscribeFromView()
        {
            _view.CloseRequestedEvent -= HandleClose;
            _view.ResetRequestedEvent -= HandleReset;
            _view.ButtonDraggedEvent -= HandleButtonDragged;
        }

        // Presenter хранит visibility у себя, полученные от View по запросу
        private void HandleAdjustmentButtonShow()
        {
            _visibility = _view.GetCurrentVisibility();
        }

        private void HandleButtonDragged(AdjustableButtonType type, Vector2 position)
        {
            _changed = true;
            _mobileControlsSettings.SetAnchoredPosition(type, position);
            _view.SetAnchoredPosition(type, position);
        }

        private void SetDefaultPosition(AdjustableButtonType type, Vector2 position)
        {
            if (_mobileControlsSettings.GetIsInitialized(type) == false)
                _mobileControlsSettings.SetDefaultAnchoredPosition(type, position);
        }

        private void HandleReset()
        {
            _mobileControlsSettings.ResetToDefaults();

            foreach (var button in _view.AdjustableButtons)
            {
                if (_mobileControlsSettings.GetIsInitialized(button.Type))
                {
                    var position = _mobileControlsSettings.GetAnchoredPosition(button.Type);
                    _changed = true;
                    _view.SetAnchoredPosition(button.Type, position);
                }
            }
        }

        private void HandleClose()
        {
            if (_changed)
                _storageDataService.Save();

            _changed = false;

            _panelManager.OpenPanel(PanelMode.Gameplay);
        }
    }
}