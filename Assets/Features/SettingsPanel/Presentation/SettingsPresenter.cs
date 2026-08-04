using Feature.Input;
using Feature.Storage;
using Feature.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using AudioSettings = Feature.Storage.AudioSettings;

namespace Feature.SettingsPanel
{
    public class SettingsPresenter : MonoBehaviour, IPanel, IInitializable, IDisposable
    {
        public List<UIPanelTag> PanelTags => Tags;

        private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
        {
            UIPanelTag.Settings
        };

        [SerializeField] private ImageButton _exitSettingsButton;

        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _joystickSlider;

        [SerializeField] private CheckBoxButton _dynamicJoystickButton;
        [SerializeField] private CheckBoxButton _followJoystickButton;

        private IUIPanelInput _input;
        private MobileControlsSettings _mobileControls;
        private ControlSettings _controlSettings;
        private MobileControlsSettings _mobileControlsSettings;
        private AudioSettings _audioSettings;
        private UIPanelsManager _panelManager;
        private IStorageDataService _storageDataService;

        private bool _changed;

        [Inject]
        private void Construct(IUIPanelInput input, IStorageDataService storageDataService, UIPanelsManager panelsManager, MobileControlsSettings mobileControls, ControlSettings controlSettings, AudioSettings audioSettings, MobileControlsSettings mobileControlsSettings)
        {
            _input = input;
            _storageDataService = storageDataService;
            _panelManager = panelsManager;
            _mobileControls = mobileControls;
            _controlSettings = controlSettings;
            _mobileControlsSettings = mobileControlsSettings;
            _audioSettings = audioSettings;
            _panelManager.AddPanel(this);
        }

        public void Initialize()
        {

        }

        public void Dispose()
        {
            _panelManager.RemovePanel(this);
        }

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _input.ToggleSettingsEvent += HandleExitSettings;
            _exitSettingsButton.Click += HandleExitSettings;

            _sensitivitySlider.onValueChanged.AddListener(HandleSensitivityListener);
            _sensitivitySlider.maxValue = _controlSettings.MaxLookSensitivity;
            _sensitivitySlider.minValue = _controlSettings.MinLookSensitivity;
            _sensitivitySlider.value = _controlSettings.LookSensitivity;

            _volumeSlider.onValueChanged.AddListener(HandleVolumeListener);
            _volumeSlider.maxValue = _audioSettings.MaxVolume;
            _volumeSlider.minValue = _audioSettings.MinVolume;
            _volumeSlider.value = _audioSettings.Volume;

            _joystickSlider.onValueChanged.AddListener(HandleJoystickListener);
            _joystickSlider.maxValue = _mobileControlsSettings.MaxJoystickRadius;
            _joystickSlider.minValue = _mobileControlsSettings.MinJoystickRadius;
            _joystickSlider.value = _mobileControlsSettings.JoystickRadius;

            _dynamicJoystickButton.Click += HandleDynamicJoystick;
            _followJoystickButton.Click += HandleFollowJoystick;

            UpdateDynamicJoystick();
            UpdateFollowJoystick();

            _changed = false;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _input.ToggleSettingsEvent -= HandleExitSettings;
            _exitSettingsButton.Click -= HandleExitSettings;

            _sensitivitySlider.onValueChanged.RemoveListener(HandleSensitivityListener);
            _volumeSlider.onValueChanged.RemoveListener(HandleVolumeListener);

            _dynamicJoystickButton.Click -= HandleDynamicJoystick;
            _followJoystickButton.Click -= HandleFollowJoystick;

            if (_changed == true)
                _storageDataService.Save();
        }

        public void Tick(float dt) { }
        private void UpdateDynamicJoystick()
        {
            _dynamicJoystickButton.SetChecked(_mobileControls.IsDynamicJoystick);
        }

        private void UpdateFollowJoystick()
        {
            _followJoystickButton.SetChecked(_mobileControls.IsFollowJoystick);
        }

        private void HandleDynamicJoystick()
        {
            _changed = true;
            _mobileControlsSettings.SetDynamicJoystick(!_mobileControlsSettings.IsDynamicJoystick);
            UpdateDynamicJoystick();
        }

        private void HandleFollowJoystick()
        {
            _changed = true;
            _mobileControlsSettings.SetFollowJoystick(!_mobileControlsSettings.IsFollowJoystick);
            UpdateFollowJoystick();
        }

        private void HandleJoystickListener(float value)
        {
            _changed = true;
            _mobileControlsSettings.SetJoystickRadius((int)value);
        }

        private void HandleSensitivityListener(float value)
        {
            _changed = true;
            _controlSettings.SetLookSensitivity((int)value);
        }

        private void HandleVolumeListener(float value)
        {
            _changed = true;
            _audioSettings.SetVolume((int)value);
        }

        private void HandleExitSettings()
        {
            _panelManager.OpenPanel(UIPanelTag.Gameplay);
        }

    }
}
