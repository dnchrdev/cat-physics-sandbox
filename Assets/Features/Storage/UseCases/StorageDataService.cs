using Cysharp.Threading.Tasks;
using Features.Shared.UseCases;
using Features.Storage.Entities;
using Features.Storage.UseCases.Ports;
using AudioSettings = Features.Storage.Entities.AudioSettings;
using ILogger = Features.Core.UseCases.Ports.ILogger;

namespace Features.Storage.UseCases
{
    public sealed class StorageDataService : IStorageDataService
    {
        private const int SaveVersion = 123;
        private const int RetryDelayMilliseconds = 1000;
        private const int LoadTimeoutMilliseconds = 5000;

        private readonly ControlSettings _controlSettings;
        private readonly MobileControlsSettings _mobileControls;
        private readonly AudioSettings _audioSettings;
        private readonly PlayerProgress _playerProgress;
        private readonly IPlayerStorage _storage;
        private readonly ILogger _logger;

        public StorageDataService(
            ControlSettings controlSettings,
            MobileControlsSettings mobileControls,
            AudioSettings audioSettings,
            PlayerProgress playerProgress,
            IPlayerStorage storage,
            ILogger logger)
        {
            _controlSettings = controlSettings;
            _mobileControls = mobileControls;
            _audioSettings = audioSettings;
            _playerProgress = playerProgress;
            _storage = storage;
            _logger = logger;
        }

        public async UniTask Load()
        {
            var elapsedMilliseconds = 0;

            while (true)
            {
                var availability = _storage.IsAvailable();
                if (availability.IsSuccess)
                {
                    var data = _storage.Read();
                    if (data.SaveVersion == SaveVersion)
                        Apply(data);
                    else
                        ResetToDefaults();
                    return;
                }

                _logger.LogWarning(GetType(), availability.Error);

                if (elapsedMilliseconds >= LoadTimeoutMilliseconds)
                {
                    _logger.LogError(GetType(),
                        $"Storage did not become valid within {LoadTimeoutMilliseconds}ms, falling back to defaults");
                    ResetToDefaults();
                    return;
                }

                await UniTask.Delay(RetryDelayMilliseconds);
                elapsedMilliseconds += RetryDelayMilliseconds;
            }
        }

        public void Save()
        {
            ButtonPositionsSaveFormat.Write(_mobileControls, out var positionsX, out var positionsY, out var isDefault);

            _storage.Write(new SaveData
            {
                SaveVersion = SaveVersion,
                TutorialCompleted = _playerProgress.IsTutorialCompleted,
                LookSensitivity = _controlSettings.LookSensitivity,
                Volume = _audioSettings.Volume,
                IsDynamicJoystick = _mobileControls.IsDynamicJoystick,
                IsFollowJoystick = _mobileControls.IsFollowJoystick,
                JoystickRadius = _mobileControls.JoystickRadius,
                PositionsX = positionsX,
                PositionsY = positionsY,
                IsDefaultPosition = isDefault
            });

            var availability = _storage.IsAvailable();
            if (!availability.IsSuccess)
            {
                _logger.LogWarning(GetType(), availability.Error);
                return;
            }

            _storage.Commit();
            _logger.Log(GetType(), "Saved");
        }

        private void Apply(SaveData data)
        {
            _logger.Log(GetType(), "Loaded saved data");
            _playerProgress.SetTutorialCompleted(data.TutorialCompleted);
            _controlSettings.SetLookSensitivity(data.LookSensitivity);
            _audioSettings.SetVolume(data.Volume);
            _mobileControls.SetDynamicJoystick(data.IsDynamicJoystick);
            _mobileControls.SetFollowJoystick(data.IsFollowJoystick);
            _mobileControls.SetJoystickRadius(data.JoystickRadius);
            ButtonPositionsSaveFormat.Read(_mobileControls, data.PositionsX, data.PositionsY, data.IsDefaultPosition);
        }

        private void ResetToDefaults()
        {
            _logger.Log(GetType(), "Loaded default data");
            _playerProgress.ResetToDefaults();
            _controlSettings.ResetToDefaults();
            _audioSettings.ResetToDefaults();
            _mobileControls.ResetToDefaults();
        }
    }
}
