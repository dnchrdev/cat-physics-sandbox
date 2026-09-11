using System;

namespace Features.SettingsPanel.Adapter.Interfaces
{
    public interface ISettingsView
    {
        event Action ExitRequestedEvent;
        event Action<float> SensitivityChangedEvent;
        event Action<float> VolumeChangedEvent;
        event Action<float> JoystickRadiusChangedEvent;
        event Action DynamicJoystickToggledEvent;
        event Action FollowJoystickToggledEvent;

        void SetActive(bool isActive);

        void SetSensitivityRange(int min, int max, int value);
        void SetVolumeRange(int min, int max, int value);
        void SetJoystickRadiusRange(int min, int max, int value);

        void SetDynamicJoystickChecked(bool isChecked);
        void SetFollowJoystickChecked(bool isChecked);
    }
}