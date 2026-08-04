using Feature.Storage;
using Feature.UI;
using System;
using Zenject;

namespace Feature.SettingsPanel
{
    public class SettingsPresenterDel
    {
        //private SettingsView _view;
        //private UIPanelsManager _panelManager;
        //private ILogger _logger;
 

        //[Inject]
        //public SettingsPresenter(SettingsView view, UIPanelsManager gameplayPanelCollection, ILogger logger)
        //{
        //    _view = view;
        //    _panelManager = gameplayPanelCollection;
        //    _logger = logger;

        //}

        //public void Initialize()
        //{
        //    var result = _panelManager.AddPanel(_view);

        //    if (!result.IsSuccess)
        //        _logger.LogError(this.GetType(), result.Message);

        //    _view.ExitSettingsEvent += HandleExitSettingsEvent;

        //    _view.OnSencitivityChangedEvent += _controlSettings.SetLookSensitivity;
        //    _view.OnVolumeChangedEvent += _audioSettings.SetVolume;
        //    _view.DynamicJoystickClecked += HandleDynamicJoystickClicked;
        //    _view.FollowJoystickClecked += HandleFollowJoystickClicked;
        //}

        //private void HandleDynamicJoystickClicked()
        //{
        //    _mobileControlsSettings.SetDynamicJoystick(!_mobileControlsSettings.IsDynamicJoystick);
        //}

        //private void HandleFollowJoystickClicked()
        //{
        //    _mobileControlsSettings.SetFollowJoystick(!_mobileControlsSettings.IsFollowJoystick);
        //}

        //public void Dispose()
        //{
        //    _panelManager.RemovePanel(_view);

        //    _view.ExitSettingsEvent -= HandleExitSettingsEvent;

        //    _view.OnSencitivityChangedEvent -= _controlSettings.SetLookSensitivity;
        //    _view.OnVolumeChangedEvent -= _audioSettings.SetVolume;
        //    _view.DynamicJoystickClecked -= HandleDynamicJoystickClicked;
        //    _view.FollowJoystickClecked -= HandleFollowJoystickClicked;
        //}

        //private void HandleExitSettingsEvent()
        //{
        //    _panelManager.OpenPanel(UIPanelTag.Gameplay);
        //}

    }
}