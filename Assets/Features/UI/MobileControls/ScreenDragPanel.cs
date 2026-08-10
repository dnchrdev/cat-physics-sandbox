using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.UI
{
    public class ScreenDragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Vector2> GetValueEvent;

        [SerializeField] private RectTransform _touchArea;

        private Vector2 _previousPointerPos;
        private Vector2 _deltaDrag = Vector2.zero;
        private bool _isDragging = false;
        
        private void Update()
        {
            if (!_isDragging)
                _deltaDrag = Vector2.zero;
            _isDragging = false;

            GetValueEvent?.Invoke(_deltaDrag);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _touchArea, eventData.position, eventData.pressEventCamera, out _previousPointerPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _isDragging = true;

            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _touchArea, eventData.position, eventData.pressEventCamera, out pointerPos);

            _deltaDrag = (pointerPos - _previousPointerPos);
            _previousPointerPos = pointerPos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _deltaDrag = Vector2.zero;
        }
    }
}