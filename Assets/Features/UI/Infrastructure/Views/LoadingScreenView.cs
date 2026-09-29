using Features.UI.InterfaceAdapters.Ports;
using UnityEngine;
using UnityEngine.UI;

namespace Features.UI.Infrastructure.Views
{

    public class LoadingScreenView : MonoBehaviour, ILoadingScreenView
    {
        [SerializeField] private GameObject _loadingPanel;
        [SerializeField] private Image _loadingScreen;
        [SerializeField] private Image _loadingCircle;

        public void ShowLoadingPanel(bool active)
        {
            _loadingPanel.SetActive(active);
        }

        public void SetLoadingScreenAlpha(float alpha)
        {
            var oldColor = _loadingScreen.color;
            _loadingScreen.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        }

        public void SetLoadingCircleAlpha(float alpha)
        {
            var oldColor = _loadingCircle.color;
            _loadingCircle.color = new Color(oldColor.r, oldColor.g, oldColor.b, alpha);
        }

        public void SetLoadingCircleZAngle(float angle)
        {
            _loadingCircle.rectTransform.eulerAngles = new Vector3(0, 0, angle);
        }

        public float GetLoadingCircleZAngle()
        {
            return _loadingCircle.rectTransform.eulerAngles.z;
        }
    }
}
