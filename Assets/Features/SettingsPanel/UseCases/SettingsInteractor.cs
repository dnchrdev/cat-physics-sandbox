using Features.SettingsPanel.UseCases.Ports;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.UseCases;

namespace Features.SettingsPanel.UseCases
{
    public sealed class SettingsInteractor
    {
        private readonly IStorageDataService _storage;
        private readonly IUIPanelsManager _panels;
        private readonly IMobileControlsSettings _mobileControls;
        private readonly IControlSettings _controlSettings;
        private readonly IAudioSettings _audioSettings;
        private readonly ISettingsOutput _output;

        private bool _hasUnsavedChanges;

        public SettingsInteractor(
            IStorageDataService storage,
            IUIPanelsManager panels,
            IMobileControlsSettings mobileControls,
            IControlSettings controlSettings,
            IAudioSettings audioSettings,
            ISettingsOutput output)
        {
            _storage = storage;
            _panels = panels;
            _mobileControls = mobileControls;
            _controlSettings = controlSettings;
            _audioSettings = audioSettings;
            _output = output;
        }

        public void ShowCurrent()
        {
            _output.ShowSettings(new SettingsSnapshot(
                sensitivityMin: _controlSettings.MinLookSensitivity,
                sensitivityMax: _controlSettings.MaxLookSensitivity,
                sensitivityValue: _controlSettings.LookSensitivity,
                volumeMin: _audioSettings.MinVolume,
                volumeMax: _audioSettings.MaxVolume,
                volumeValue: _audioSettings.Volume,
                joystickRadiusMin: _mobileControls.MinJoystickRadius,
                joystickRadiusMax: _mobileControls.MaxJoystickRadius,
                joystickRadiusValue: _mobileControls.JoystickRadius,
                isDynamicJoystick: _mobileControls.IsDynamicJoystick,
                isFollowJoystick: _mobileControls.IsFollowJoystick));

            _hasUnsavedChanges = false;
        }

        public void SetSensitivity(int value)
        {
            _controlSettings.SetLookSensitivity(value);
            _hasUnsavedChanges = true;
        }

        public void SetVolume(int value)
        {
            _audioSettings.SetVolume(value);
            _hasUnsavedChanges = true;
        }

        public void SetJoystickRadius(int value)
        {
            _mobileControls.SetJoystickRadius(value);
            _hasUnsavedChanges = true;
        }

        public void ToggleDynamicJoystick()
        {
            _mobileControls.SetDynamicJoystick(!_mobileControls.IsDynamicJoystick);
            _hasUnsavedChanges = true;
            _output.ShowDynamicJoystick(_mobileControls.IsDynamicJoystick);
        }

        public void ToggleFollowJoystick()
        {
            _mobileControls.SetFollowJoystick(!_mobileControls.IsFollowJoystick);
            _hasUnsavedChanges = true;
            _output.ShowFollowJoystick(_mobileControls.IsFollowJoystick);
        }

        public void Close()
        {
            if (_hasUnsavedChanges)
                _storage.Save();

            _hasUnsavedChanges = false;
            _panels.OpenPanel(PanelMode.Gameplay);
        }
    }
}
