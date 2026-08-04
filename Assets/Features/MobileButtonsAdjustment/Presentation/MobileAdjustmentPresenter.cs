using Feature.PlayerFeature;
using Feature.Storage;
using Feature.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Feature.MobileButtonsAdjustment
{
    [Serializable]
    public enum AdjustableButtonType
    {
        Hit, Grab, Throw, Release, MoveJoystick, Jump
    }

    [Serializable]
    public struct AdjustableButtonsMap
    {
        public AdjustableButtonType Type;
        public GameObject Button;
        public DraggableUIElement Draggablebutton;
    }

    public class MobileAdjustmentPresenter : MonoBehaviour, IPanel, IInitializable, IDisposable
    {
        [SerializeField] private ImageButton _closeMobileAdjustmentButton;
        [SerializeField] private ImageButton _resetAdjustmentButton;

        [SerializeField] private List<AdjustableButtonsMap> _draggableButtons;

        private Dictionary<AdjustableButtonsMap, bool> _visibleButtons = new();
        private readonly Dictionary<DraggableUIElement, Action<Vector2>> _dragHandlers = new();

        public List<AdjustableButtonsMap> AdjustableButtons => _draggableButtons;

        public List<UIPanelTag> PanelTags => Tags;
        private readonly List<UIPanelTag> Tags = new() { UIPanelTag.MobileButtonAdjustment };

        private IStorageDataService _storageDataService;
        private MobileControlsSettings _mobileControlsSettings;
        private IReadOnlyControlSettings _controlSettings;
        private UIPanelsManager _panelManager;
        private MobileControlPresenter _mobileControllers;

        private bool _changed;

        [Inject]
        private void Construct(
            IStorageDataService storageDataService, 
            MobileControlsSettings readOnlyMobileControls,
            IReadOnlyControlSettings controlSettings,
            UIPanelsManager gameplayPanelCollection,
            MobileControlPresenter mobileControllers
            )
        {
            _storageDataService = storageDataService;
            _mobileControlsSettings = readOnlyMobileControls;
            _controlSettings = controlSettings;
            _panelManager = gameplayPanelCollection;
            _mobileControllers = mobileControllers;

            //if (_controlSettings.IsMobile)
            //{
                _panelManager.AddPanel(this);

                foreach (var btn in AdjustableButtons)
                {
                    SetDefaultPosition(btn.Type, btn.Draggablebutton.GetAnchoredPosition());
                }
            //}
        }

        public void Initialize()
        {
            //if (_controlSettings.IsMobile)
            //{
                _mobileControllers.OnPanelExit += UpdateVisibleButtons;
            //}
        }

        public void Dispose()
        {
            //if (_controlSettings.IsMobile)
            //{
                _mobileControllers.OnPanelExit -= UpdateVisibleButtons;

                _panelManager.RemovePanel(this);
            //}
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _closeMobileAdjustmentButton.Click += OpenGameplayPanel;
            _resetAdjustmentButton.Click += HandleAdjustmentReset;

            foreach (var button in _visibleButtons)
            {
                button.Key.Draggablebutton.gameObject.SetActive(button.Value);

                var draggableButton = button.Key.Draggablebutton;

                SetAnchoredPosition(button.Key.Type, draggableButton.GetRectTransform(), _mobileControlsSettings.GetAnchoredPosition(button.Key.Type));

                if (button.Value == false) continue;


                Action<Vector2> handler = pos => SetAnchoredPosition(button.Key.Type, draggableButton.GetRectTransform(), pos);
                _dragHandlers[draggableButton] = handler;
                draggableButton.OnDragUpdate += handler;
            }

            _changed = false;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _closeMobileAdjustmentButton.Click -= OpenGameplayPanel;
            _resetAdjustmentButton.Click -= HandleAdjustmentReset;

            foreach (var (btn, handler) in _dragHandlers)
                btn.OnDragUpdate -= handler;

            _dragHandlers.Clear();

            if(_changed == true)
                _storageDataService.Save();
        }

        public void Tick(float dt) { }

        private void UpdateVisibleButtons()
        {
            _visibleButtons = new();

            foreach (var draggableButton in _draggableButtons)
            {
                _visibleButtons.Add(draggableButton, draggableButton.Button.activeSelf);
            }
        }

        private void SetAnchoredPosition(AdjustableButtonType type, RectTransform element, Vector2 position)
        {
            _changed = true;
            element.anchoredPosition = position;
            _mobileControlsSettings.SetAnchoredPosition(type, position);
        }

        private void SetDefaultPosition(AdjustableButtonType type, Vector2 position)
        {
            if (_mobileControlsSettings.GetIsInitialized(type) == false)
            {
                _mobileControlsSettings.SetDefaultAnchoredPosition(type, position);
            }
        }

        private void HandleAdjustmentReset()
        {
            _mobileControlsSettings.ResetToDefaults();

            foreach (var draggableButton in AdjustableButtons)
            {
                if (_mobileControlsSettings.GetIsInitialized(draggableButton.Type))
                    SetAnchoredPosition(
                        draggableButton.Type,
                        draggableButton.Draggablebutton.GetRectTransform(),
                        _mobileControlsSettings.GetAnchoredPosition(draggableButton.Type
                        ));
            }
        }

        private void OpenGameplayPanel()
        {
            _panelManager.OpenPanel(UIPanelTag.Gameplay);
        }

    }
}
