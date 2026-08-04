using Feature.Scene;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.UI
{
    public class LoadLevelButton : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public event Action<ScenesConfig> Click;
        public event Action Down;
        public event Action Up;

        [SerializeField] private ScenesConfig _sceneObject;
        [SerializeField] private GameObject _highlight;

        private void Awake()
        {
            if (_highlight != null)
                _highlight.SetActive(false);
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

        public void OnPointerClick(PointerEventData eventData)
        {
            Click?.Invoke(_sceneObject);
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
    }
}
