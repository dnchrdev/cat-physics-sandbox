using System.Collections.Generic;
using Feature.CameraFeature;
using Feature.PhysicsInteraction;
using Feature.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractionIndication : MonoBehaviour, IPanel
{
    [SerializeField] private GameObject _indication;
    [SerializeField] private Image _focusedImage;
    [SerializeField] private Image _grabbedImage;

    [Inject] private readonly InteractionControllerConfig _config;
    [Inject] private readonly IReadOnlyCamera _readOnlyCamera;
    
    private bool _active;
    private bool _isFocusedImage;
    
    public PanelMode[] PanelModes => new[] { PanelMode.Gameplay };
    public PanelInput PanelInput => PanelInput.All;
 
    public void InitPanel()
    {
        _active = true;
        HideIndication();
    }

    public void OnEnterPanel()
    {
        gameObject.SetActive(true);
    }

    public void OnExitPanel()
    {
        gameObject.SetActive(false);
    }

    public void UpdateGrabIndicationPosition(Vector3 aimigPosition)
    {
        var pos = _readOnlyCamera.Camera.WorldToScreenPoint(aimigPosition);
        if (pos.z < 0)
            pos *= 1;

        bool isGrabIndicationOnScreen =
            Vector3.Angle(_readOnlyCamera.Forward, aimigPosition - _readOnlyCamera.Position) < 90f;

        if (isGrabIndicationOnScreen)
            _indication.transform.position = pos;
    }

    public void SwitchGrabIndicationImage(bool isFocused)
    {
        if (isFocused)
        {
            if (_isFocusedImage) return;

            _isFocusedImage = true;
            _focusedImage.gameObject.SetActive(true);
            _grabbedImage.gameObject.SetActive(false);
        }
        else
        {
            if (_isFocusedImage == false) return;

            _isFocusedImage = false;
            _focusedImage.gameObject.SetActive(false);
            _grabbedImage.gameObject.SetActive(true);
        }
    }

    public void ShowGrabIndication(Vector3 aimigPosition)
    {
        if (_active) return;
        _active = true;
        UpdateGrabIndicationPosition(aimigPosition);
        _indication.gameObject.SetActive(true);
    }

    public void HideIndication()
    {
        if (_active == false) return;
        _active = false;

        _indication.gameObject.SetActive(false);
    }
}