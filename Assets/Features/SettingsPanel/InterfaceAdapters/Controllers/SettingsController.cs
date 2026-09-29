using System;
using Features.SettingsPanel.InterfaceAdapters.Ports;
using Features.SettingsPanel.UseCases;
using Zenject;

namespace Features.SettingsPanel.InterfaceAdapters.Controllers
{
    public sealed class SettingsController : IInitializable, IDisposable
    {
        private readonly ISettingsView _view;
        private readonly SettingsInteractor _settings;

        public SettingsController(ISettingsView view, SettingsInteractor settings)
        {
            _view = view;
            _settings = settings;
        }

        public void Initialize()
        {
            _view.ExitRequestedEvent += _settings.Close;
            _view.SensitivityChangedEvent += OnSensitivityChanged;
            _view.VolumeChangedEvent += OnVolumeChanged;
            _view.JoystickRadiusChangedEvent += OnJoystickRadiusChanged;
            _view.DynamicJoystickToggledEvent += _settings.ToggleDynamicJoystick;
            _view.FollowJoystickToggledEvent += _settings.ToggleFollowJoystick;

            _settings.ShowCurrent();
        }

        public void Dispose()
        {
            _view.ExitRequestedEvent -= _settings.Close;
            _view.SensitivityChangedEvent -= OnSensitivityChanged;
            _view.VolumeChangedEvent -= OnVolumeChanged;
            _view.JoystickRadiusChangedEvent -= OnJoystickRadiusChanged;
            _view.DynamicJoystickToggledEvent -= _settings.ToggleDynamicJoystick;
            _view.FollowJoystickToggledEvent -= _settings.ToggleFollowJoystick;
        }

        private void OnSensitivityChanged(float value) => _settings.SetSensitivity((int)value);
        private void OnVolumeChanged(float value) => _settings.SetVolume((int)value);
        private void OnJoystickRadiusChanged(float value) => _settings.SetJoystickRadius((int)value);
    }
}
