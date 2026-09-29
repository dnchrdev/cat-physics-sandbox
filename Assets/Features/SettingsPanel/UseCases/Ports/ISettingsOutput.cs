namespace Features.SettingsPanel.UseCases.Ports
{
    public interface ISettingsOutput
    {
        void ShowSettings(SettingsSnapshot settings);
        void ShowDynamicJoystick(bool isEnabled);
        void ShowFollowJoystick(bool isEnabled);
    }
}
