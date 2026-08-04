using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.UI
{
    public class ImageButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action Click;
        public event Action Down;
        public event Action Up;

        [SerializeField] private GameObject _highlight;
        [SerializeField] private GameObject _greyOverlay;

        private RectTransform _rectTransform;

        private bool _enabled = true;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();

            if (_highlight != null)
                _highlight.SetActive(false);

            _enabled = true;
        }

        private void OnEnable()
        {
            if (_highlight != null)
                _highlight?.SetActive(false);
        }

        private void OnDisable()
        {
            if (_highlight != null)
                _highlight?.SetActive(false);
        }

        public void Enable()
        {
            _enabled = true;
            _greyOverlay?.SetActive(false);
        }

        public void Disable()
        {
            _enabled = false;
            _greyOverlay?.SetActive(true);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Down?.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_highlight != null)
                _highlight?.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_highlight != null)
                _highlight?.SetActive(false);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Up?.Invoke();
        }

        public void SetAnchoredPosition(Vector2 pos)
        {
            if(_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();

            _rectTransform.anchoredPosition = pos;
        }
    }
}