using Features.Shared.Utilities;
using Features.Storage.UseCases.Ports;
using YG;

namespace YG
{
    public partial class SavesYG
    {
        public int SavedID;
        public bool TutorialCompleted;
        public int LookSensitivity;
        public int AudioVolume;
        public bool IsDynamicJoystick;
        public bool IsFollowJoystick;
        public int JoystickRadius;
        public float[] PositionsX;
        public float[] PositionsY;
        public bool[] IsDefault;
    }
}

namespace Features.Storage.Infrastructure
{
    public sealed class PlayerStorage : IPlayerStorage
    {
        public Result IsAvailable() =>
            YG2.isSDKEnabled ? Result.Success() : Result.Failure("Yandex SDK is not valid");

        public SaveData Read()
        {
            var saves = YG2.saves;
            return new SaveData
            {
                SaveVersion = saves.SavedID,
                TutorialCompleted = saves.TutorialCompleted,
                LookSensitivity = saves.LookSensitivity,
                Volume = saves.AudioVolume,
                IsDynamicJoystick = saves.IsDynamicJoystick,
                IsFollowJoystick = saves.IsFollowJoystick,
                JoystickRadius = saves.JoystickRadius,
                PositionsX = saves.PositionsX,
                PositionsY = saves.PositionsY,
                IsDefaultPosition = saves.IsDefault
            };
        }

        public void Write(SaveData data)
        {
            var saves = YG2.saves;
            saves.SavedID = data.SaveVersion;
            saves.TutorialCompleted = data.TutorialCompleted;
            saves.LookSensitivity = data.LookSensitivity;
            saves.AudioVolume = data.Volume;
            saves.IsDynamicJoystick = data.IsDynamicJoystick;
            saves.IsFollowJoystick = data.IsFollowJoystick;
            saves.JoystickRadius = data.JoystickRadius;
            saves.PositionsX = data.PositionsX;
            saves.PositionsY = data.PositionsY;
            saves.IsDefault = data.IsDefaultPosition;
        }

        public void Commit() => YG2.SaveProgress();
    }
}