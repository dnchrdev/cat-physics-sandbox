using System;
using Feature.Storage;
using Feature.UI;
using Zenject;
using AudioSettings = Feature.Storage.AudioSettings;

namespace Feature.SettingsPanel
{
    public class SettingsPresenter : IInitializable, IDisposable
    {
        [Inject] private readonly IStorageDataService _storageDataService;
        [Inject] private readonly UIPanelsManager _panelManager;
        [Inject] private readonly MobileControlsSettings _mobileControlsSettings;
        [Inject] private readonly ControlSettings _controlSettings;
        [Inject] private readonly AudioSettings _audioSettings;
        [Inject] private readonly ISettingsView _view;

        private bool _changed;

        public void Initialize()
        {
            _view.ExitRequestedEvent += HandleExitSettings;
            _view.SensitivityChangedEvent += HandleSensitivityChanged;
            _view.VolumeChangedEvent += HandleVolumeChanged;
            _view.JoystickRadiusChangedEvent += HandleJoystickRadiusChanged;
            _view.DynamicJoystickToggledEvent += HandleDynamicJoystickToggled;
            _view.FollowJoystickToggledEvent += HandleFollowJoystickToggled;

            ApplyInitialValues();
        }

        public void Dispose()
        {
            _view.ExitRequestedEvent -= HandleExitSettings;
            _view.SensitivityChangedEvent -= HandleSensitivityChanged;
            _view.VolumeChangedEvent -= HandleVolumeChanged;
            _view.JoystickRadiusChangedEvent -= HandleJoystickRadiusChanged;
            _view.DynamicJoystickToggledEvent -= HandleDynamicJoystickToggled;
            _view.FollowJoystickToggledEvent -= HandleFollowJoystickToggled;
        }

        private void ApplyInitialValues()
        {
            _view.SetSensitivityRange(
                _controlSettings.MinLookSensitivity,
                _controlSettings.MaxLookSensitivity,
                _controlSettings.LookSensitivity);

            _view.SetVolumeRange(
                _audioSettings.MinVolume,
                _audioSettings.MaxVolume,
                _audioSettings.Volume);

            _view.SetJoystickRadiusRange(
                _mobileControlsSettings.MinJoystickRadius,
                _mobileControlsSettings.MaxJoystickRadius,
                _mobileControlsSettings.JoystickRadius);

            _view.SetDynamicJoystickChecked(_mobileControlsSettings.IsDynamicJoystick);
            _view.SetFollowJoystickChecked(_mobileControlsSettings.IsFollowJoystick);

            _changed = false;
        }

        private void HandleDynamicJoystickToggled()
        {
            _changed = true;
            _mobileControlsSettings.SetDynamicJoystick(!_mobileControlsSettings.IsDynamicJoystick);
            _view.SetDynamicJoystickChecked(_mobileControlsSettings.IsDynamicJoystick);
        }

        private void HandleFollowJoystickToggled()
        {
            _changed = true;
            _mobileControlsSettings.SetFollowJoystick(!_mobileControlsSettings.IsFollowJoystick);
            _view.SetFollowJoystickChecked(_mobileControlsSettings.IsFollowJoystick);
        }

        private void HandleJoystickRadiusChanged(float value)
        {
            _changed = true;
            _mobileControlsSettings.SetJoystickRadius((int)value);
        }

        private void HandleSensitivityChanged(float value)
        {
            _changed = true;
            _controlSettings.SetLookSensitivity((int)value);
        }

        private void HandleVolumeChanged(float value)
        {
            _changed = true;
            _audioSettings.SetVolume((int)value);
        }

        private void HandleExitSettings()
        {
            if (_changed)
                _storageDataService.Save();

            _changed = false;

            _panelManager.OpenPanel(PanelMode.Gameplay);
        }
    }
}