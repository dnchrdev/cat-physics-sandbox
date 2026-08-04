using Feature.Core;
using YG;

namespace YG
{
    public partial class SavesYG
    {
        public int SavedID;
        public bool TutorialCompleted;
        public int LookSensitivity;
        public int AudioVolume;
        public bool IsDymanicJoystick;
        public bool IsFollowJoystick;
        public int JoystickRadius;
        public float[] PositionsX;
        public float[] PositionsY;
        public bool[] IsDefault;
    }
}

public class PlayerStorage : IPlayerStorage
{

    public float[] GetAnchoredPositionsX()
    {
        return YG2.saves.PositionsX;
    }
    
    public float[] GetAnchoredPositionsY()
    {
        return YG2.saves.PositionsY;
    }
    
    
    public bool[] GetDefaults()
    {
        return YG2.saves.IsDefault;
    }
    
    public bool GetIsJoystickDynamic()
    {
        return YG2.saves.IsDymanicJoystick;
    }
    
    public bool GetIsJoystickFollow()
    {
        return YG2.saves.IsFollowJoystick;
    }
    
    public int GetJoystickRadius()
    {
        return YG2.saves.JoystickRadius;
    }
    
    public int GetLookSensitivity()
    {
        return YG2.saves.LookSensitivity;
    }
    
    public int GetSavedID()
    {
        return YG2.saves.SavedID;
    }
    
    public int GetSoundVolume()
    {
        return YG2.saves.AudioVolume;
    }
    
    public bool GetTutorialCompleted()
    {
        return YG2.saves.TutorialCompleted;
    }

    public Result IsValid()
    {
        if (YG2.isSDKEnabled)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure("Yandex SDK is not valid");
        }
    }
    
    public void Save()
    {
        YG2.SaveProgress();
    }
    
    public void SetAnchoredPositions(float[] positionsX, float[] positionsY)
    {
        YG2.saves.PositionsX = positionsX;
        YG2.saves.PositionsY = positionsY;
    }
    
    public void SetDefaults(bool[] defaults)
    {
        YG2.saves.IsDefault = defaults;
    }
    
    public void SetIsJoystickDynamic(bool dynamic)
    {
        YG2.saves.IsDymanicJoystick = dynamic;
    }
    
    public void SetIsJoystickFollow(bool follow)
    {
        YG2.saves.IsFollowJoystick = follow;
    }
    
    public void SetJoystickRadius(int radius)
    {
        YG2.saves.JoystickRadius = radius; 
    }
    
    public void SetLookSensitivity(int sensitivity)
    {
        YG2.saves.LookSensitivity = sensitivity;
    }
    
    public void SetSavedID(int id)
    {
        YG2.saves.SavedID = id;
    }
    
    public void SetSoundVolume(int volume)
    {
        YG2.saves.AudioVolume = volume;
    }
    
    public void SetTutorialCompleted(bool completed)
    {
        YG2.saves.TutorialCompleted = completed;
    }
}
