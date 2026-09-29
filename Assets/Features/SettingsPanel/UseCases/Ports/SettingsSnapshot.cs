namespace Features.SettingsPanel.UseCases.Ports
{
    public sealed class SettingsSnapshot
    {
        public int SensitivityMin { get; }
        public int SensitivityMax { get; }
        public int SensitivityValue { get; }

        public int VolumeMin { get; }
        public int VolumeMax { get; }
        public int VolumeValue { get; }

        public int JoystickRadiusMin { get; }
        public int JoystickRadiusMax { get; }
        public int JoystickRadiusValue { get; }

        public bool IsDynamicJoystick { get; }
        public bool IsFollowJoystick { get; }

        public SettingsSnapshot(
            int sensitivityMin, int sensitivityMax, int sensitivityValue,
            int volumeMin, int volumeMax, int volumeValue,
            int joystickRadiusMin, int joystickRadiusMax, int joystickRadiusValue,
            bool isDynamicJoystick, bool isFollowJoystick)
        {
            SensitivityMin = sensitivityMin;
            SensitivityMax = sensitivityMax;
            SensitivityValue = sensitivityValue;

            VolumeMin = volumeMin;
            VolumeMax = volumeMax;
            VolumeValue = volumeValue;

            JoystickRadiusMin = joystickRadiusMin;
            JoystickRadiusMax = joystickRadiusMax;
            JoystickRadiusValue = joystickRadiusValue;

            IsDynamicJoystick = isDynamicJoystick;
            IsFollowJoystick = isFollowJoystick;
        }
    }
}
