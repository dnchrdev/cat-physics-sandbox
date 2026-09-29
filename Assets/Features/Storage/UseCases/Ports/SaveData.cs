namespace Features.Storage.UseCases.Ports
{
    public sealed class SaveData
    {
        public int SaveVersion;
        public bool TutorialCompleted;
        public int LookSensitivity;
        public int Volume;
        public bool IsDynamicJoystick;
        public bool IsFollowJoystick;
        public int JoystickRadius;
        public float[] PositionsX;
        public float[] PositionsY;
        public bool[] IsDefaultPosition;
    }
}
