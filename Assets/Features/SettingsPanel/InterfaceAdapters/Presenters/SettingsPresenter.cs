using Features.SettingsPanel.InterfaceAdapters.Ports;
using Features.SettingsPanel.UseCases.Ports;

namespace Features.SettingsPanel.InterfaceAdapters.Presenters
{
    public sealed class SettingsPresenter : ISettingsOutput
    {
        private readonly ISettingsView _view;

        public SettingsPresenter(ISettingsView view)
        {
            _view = view;
        }

        public void ShowSettings(SettingsSnapshot settings)
        {
            _view.SetSensitivityRange(settings.SensitivityMin, settings.SensitivityMax, settings.SensitivityValue);
            _view.SetVolumeRange(settings.VolumeMin, settings.VolumeMax, settings.VolumeValue);
            _view.SetJoystickRadiusRange(settings.JoystickRadiusMin, settings.JoystickRadiusMax, settings.JoystickRadiusValue);
            _view.SetDynamicJoystickChecked(settings.IsDynamicJoystick);
            _view.SetFollowJoystickChecked(settings.IsFollowJoystick);
        }

        public void ShowDynamicJoystick(bool isEnabled) => _view.SetDynamicJoystickChecked(isEnabled);
        public void ShowFollowJoystick(bool isEnabled) => _view.SetFollowJoystickChecked(isEnabled);
    }
}
