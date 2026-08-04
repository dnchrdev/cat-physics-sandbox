using Feature.Core;
using Feature.Storage;

public interface IPlayerStorage
{
    void SetSavedID(int id);
    int GetSavedID();

    void SetTutorialCompleted(bool completed);
    bool GetTutorialCompleted();

    void SetLookSensitivity(int sensitivity);
    int GetLookSensitivity();

    void SetSoundVolume(int volume);
    int GetSoundVolume();

    void SetIsJoystickDynamic(bool dynamic);
    bool GetIsJoystickDynamic();

    void SetIsJoystickFollow(bool follow);
    bool GetIsJoystickFollow();

    void SetJoystickRadius(int radius);
    int GetJoystickRadius();

    void SetAnchoredPositions(float[] positionsX, float[] positionsY);
    float[] GetAnchoredPositionsX();
    float[] GetAnchoredPositionsY();

    void SetDefaults(bool[] defaults);
    bool[] GetDefaults();

    void Save();
    Result IsValid();
}
