using System;
using System.Collections.Generic;
using Feature.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Feature.SettingsPanel
{
    public class SettingsView : MonoBehaviour, ISettingsView, IPanel
    {
        public event Action ExitRequestedEvent;
        public event Action<float> SensitivityChangedEvent;
        public event Action<float> VolumeChangedEvent;
        public event Action<float> JoystickRadiusChangedEvent;
        public event Action DynamicJoystickToggledEvent;
        public event Action FollowJoystickToggledEvent;

        public PanelMode[] PanelModes => new [] { PanelMode.Settings };
        public PanelInput PanelInput => PanelInput.All;

        [SerializeField] private ImageButton _exitSettingsButton;

        [SerializeField] private Slider _sensitivitySlider;
        [SerializeField] private Slider _volumeSlider;
        [SerializeField] private Slider _joystickSlider;

        [SerializeField] private CheckBoxButton _dynamicJoystickButton;
        [SerializeField] private CheckBoxButton _followJoystickButton;

        public void InitPanel()
        {
            gameObject.SetActive(false);
        }

        public void OnEnterPanel()
        {
            gameObject.SetActive(true);

            _exitSettingsButton.Click += OnExitButtonClicked;

            _sensitivitySlider.onValueChanged.AddListener(OnSensitivityChanged);
            _volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            _joystickSlider.onValueChanged.AddListener(OnJoystickRadiusChanged);

            _dynamicJoystickButton.Click += OnDynamicJoystickClicked;
            _followJoystickButton.Click += OnFollowJoystickClicked;
        }

        public void OnExitPanel()
        {
            gameObject.SetActive(false);

            _exitSettingsButton.Click -= OnExitButtonClicked;

            _sensitivitySlider.onValueChanged.RemoveListener(OnSensitivityChanged);
            _volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            _joystickSlider.onValueChanged.RemoveListener(OnJoystickRadiusChanged);

            _dynamicJoystickButton.Click -= OnDynamicJoystickClicked;
            _followJoystickButton.Click -= OnFollowJoystickClicked;
        }

        public void Tick(float dt)
        {
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        public void SetSensitivityRange(int min, int max, int value)
        {
            _sensitivitySlider.minValue = min;
            _sensitivitySlider.maxValue = max;
            _sensitivitySlider.value = value;
        }

        public void SetVolumeRange(int min, int max, int value)
        {
            _volumeSlider.minValue = min;
            _volumeSlider.maxValue = max;
            _volumeSlider.value = value;
        }

        public void SetJoystickRadiusRange(int min, int max, int value)
        {
            _joystickSlider.minValue = min;
            _joystickSlider.maxValue = max;
            _joystickSlider.value = value;
        }

        public void SetDynamicJoystickChecked(bool isChecked)
        {
            _dynamicJoystickButton.SetChecked(isChecked);
        }

        public void SetFollowJoystickChecked(bool isChecked)
        {
            _followJoystickButton.SetChecked(isChecked);
        }

        private void OnExitButtonClicked() => ExitRequestedEvent?.Invoke();
        private void OnSensitivityChanged(float value) => SensitivityChangedEvent?.Invoke(value);
        private void OnVolumeChanged(float value) => VolumeChangedEvent?.Invoke(value);
        private void OnJoystickRadiusChanged(float value) => JoystickRadiusChangedEvent?.Invoke(value);
        private void OnDynamicJoystickClicked() => DynamicJoystickToggledEvent?.Invoke();
        private void OnFollowJoystickClicked() => FollowJoystickToggledEvent?.Invoke();
    }
}