using Cysharp.Threading.Tasks;
using DG.Tweening;
using Feature.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreenView : MonoBehaviour, ILoadingScreenView
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private Image _loadingScreen;
    [SerializeField] private Image _loadingCircle;

    public void SetLoadingScreenAlpha(float alpha)
    {
        var oldColor = _loadingScreen.color;
        _loadingScreen.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
    }

    public void ShowLoadingPanel(bool active)
    {
        _loadingPanel.gameObject.SetActive(active);
    }

    public void SetLoadingCircleZAngle(float angle)
    {
        _loadingCircle.rectTransform.eulerAngles = new Vector3(0, 0, angle);
    }

    public void SetLoadingCircleAlpha(float alpha)
    {
        var oldColor = _loadingCircle.color;
        _loadingCircle.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
    }

    public float GetLoadingCircleZAngle()
    {
        return _loadingCircle.rectTransform.eulerAngles.z;
    }
}
