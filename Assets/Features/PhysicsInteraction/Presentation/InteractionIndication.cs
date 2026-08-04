using System.Collections.Generic;
using Feature.CameraFeature;
using Feature.PhysicsInteraction;
using Feature.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InteractionIndication : MonoBehaviour, IPanel
{
    public List<UIPanelTag> PanelTags => Tags;

    private readonly List<UIPanelTag> Tags = new List<UIPanelTag>
    {
        UIPanelTag.Gameplay
    };

    [SerializeField] private GameObject _indication;
    [SerializeField] private Image _focusedImage;
    [SerializeField] private Image _grabbedImage;

    private InteractionControllerConfig _config;
    private IReadOnlyCamera _readOnlyCamera;
    private bool _active;
    private bool _isFocusedImage;

    [Inject]
    public void Construct(IReadOnlyCamera readOnlyCamera, InteractionControllerConfig config)
    {
        _readOnlyCamera = readOnlyCamera;
        _config = config;
        _active = true;
        HideIndication();
    }

    public void InitPanel()
    {
        gameObject.SetActive(false);
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