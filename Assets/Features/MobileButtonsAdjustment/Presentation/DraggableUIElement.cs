using System.Collections;
using UnityEngine;
using System;
using UnityEngine.EventSystems;

namespace Feature.MobileButtonsAdjustment
{ 
    [RequireComponent(typeof(RectTransform))]
    public class DraggableUIElement : MonoBehaviour,
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action OnDragBegin;
        public event Action<Vector2> OnDragUpdate;
        public event Action OnDragEnd;

        private RectTransform _rectTransform;
       // [SerializeField] private Canvas _canvas;

        private Vector2 _pointerOffset;

        public void OnPointerDown(PointerEventData eventData)
        {
            // Оба в пространстве родителя
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _rectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out Vector2 pointerInParent
            );

            _pointerOffset = pointerInParent - _rectTransform.anchoredPosition;
            OnDragBegin?.Invoke();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _rectTransform.parent as RectTransform,
                    eventData.position,
                    eventData.pressEventCamera,
                    out Vector2 localPoint)) return;

            OnDragUpdate?.Invoke(localPoint - _pointerOffset);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnDragEnd?.Invoke();
        }

        public RectTransform GetRectTransform()
        {
            if(_rectTransform == null)
                _rectTransform = GetComponent<RectTransform>();
            return _rectTransform;
        }
        public Vector2 GetAnchoredPosition() => GetRectTransform().anchoredPosition;
    }
}