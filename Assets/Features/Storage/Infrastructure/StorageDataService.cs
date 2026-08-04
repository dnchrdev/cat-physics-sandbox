using Cysharp.Threading.Tasks;
using Feature.MobileButtonsAdjustment;
using Feature.Storage;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using AudioSettings = Feature.Storage.AudioSettings;


public class StorageDataService : IStorageDataService
{
    private int _saveID;
    private ControlSettings _controlSettings;
    private MobileControlsSettings _mobileControlsSettings;
    private AudioSettings _audioSettings;
    private PlayerProgress _playerProgress;
    private IPlayerStorage _playerStorage;
    private ILogger _logger;

    public StorageDataService(ControlSettings controlSettings, MobileControlsSettings mobileControlsSettings, AudioSettings audioSettings, PlayerProgress playerProgress, IPlayerStorage playerStorage, ILogger logger)
    {
        _controlSettings = controlSettings;
        _mobileControlsSettings = mobileControlsSettings;
        _audioSettings = audioSettings;
        _playerProgress = playerProgress;
        _playerStorage = playerStorage;
        _logger = logger;
    }

    public void InitSaveID(int saveID)
    {
        _saveID = saveID;
    }

    public void Load(Action callback)
    {
        HandleLoadAsync(callback).Forget();
    }

    private async UniTask HandleLoadAsync(Action callback = null)
    {
        var result = _playerStorage.IsValid();

        if (result.IsSuccess)
        {
            if (_playerStorage.GetSavedID() == _saveID)
            {
                LoadSavedData();
            }
            else
            {
                LoadDefaultData();
            }

            callback?.Invoke();
        }
        else
        {
            _logger.LogWarning(this.GetType(), result.Message);
            await UniTask.Delay(100);
            HandleLoadAsync(callback).Forget();
        }
    }

    private void LoadSavedData()
    {
        _logger.LogWarning(this.GetType(), "LoadSavedData");

        _playerProgress.SetTutorialCompleted(_playerStorage.GetTutorialCompleted());

        _controlSettings.SetLookSensitivity(_playerStorage.GetLookSensitivity());
        _audioSettings.SetVolume(_playerStorage.GetSoundVolume());

        _mobileControlsSettings.SetDynamicJoystick(_playerStorage.GetIsJoystickDynamic());
        _mobileControlsSettings.SetFollowJoystick(_playerStorage.GetIsJoystickFollow());
        _mobileControlsSettings.SetJoystickRadius(_playerStorage.GetJoystickRadius());

        _mobileControlsSettings.SetAnchoredPositions(_playerStorage.GetAnchoredPositionsX(), _playerStorage.GetAnchoredPositionsY(), _playerStorage.GetDefaults());

    }

    private void LoadDefaultData()
    {
        _logger.LogWarning(this.GetType(), "LoadDefaultData");

        _playerProgress.SetTutorialCompleted(false);

        _controlSettings.SetLookSensitivity(50);
        _audioSettings.SetVolume(50);

        _mobileControlsSettings.SetDynamicJoystick(true);
        _mobileControlsSettings.SetFollowJoystick(true);
        _mobileControlsSettings.SetJoystickRadius(125);

        _mobileControlsSettings.ResetAllPositions();
    }

    public void Save()
    {
        _playerStorage.SetSavedID(_saveID);

        _playerStorage.SetTutorialCompleted(_playerProgress.IsTutorialCompleted);

        _playerStorage.SetLookSensitivity(_controlSettings.LookSensitivity);
        _playerStorage.SetSoundVolume(_audioSettings.Volume);

        _playerStorage.SetIsJoystickDynamic(_mobileControlsSettings.IsDynamicJoystick);
        _playerStorage.SetIsJoystickFollow(_mobileControlsSettings.IsFollowJoystick);
        _playerStorage.SetJoystickRadius(_mobileControlsSettings.JoystickRadius);

        List<float> positionsX = new();
        List<float> positionsY = new();

        foreach (var item in _mobileControlsSettings.AdjustablePositions)
        {
            positionsX.Add(item.X);
            positionsY.Add(item.Y);
        }

        _playerStorage.SetAnchoredPositions(positionsX.ToArray(), positionsY.ToArray());
        _playerStorage.SetDefaults(_mobileControlsSettings.Defaults);

        var result = _playerStorage.IsValid();

        if (!result.IsSuccess)
        {
            _logger.LogWarning(this.GetType(), result.Message);
            return;
        }

        _logger.LogWarning(this.GetType(), "SAVED");
        _playerStorage.Save();
    }
}
