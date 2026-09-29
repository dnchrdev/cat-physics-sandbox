using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.MobileButtonsAdjustment.Infrastructure
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableUIElement : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        public event Action<Vector2> Dragged;

        private RectTransform _rectTransform;
        private Vector2 _pointerOffset;

        private RectTransform RectTransform =>
            _rectTransform != null ? _rectTransform : _rectTransform = GetComponent<RectTransform>();

        public Vector2 AnchoredPosition => RectTransform.anchoredPosition;

        public void SetAnchoredPosition(Vector2 position) => RectTransform.anchoredPosition = position;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (TryGetPointerInParent(eventData, out var pointerInParent))
                _pointerOffset = pointerInParent - RectTransform.anchoredPosition;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (TryGetPointerInParent(eventData, out var pointerInParent))
                Dragged?.Invoke(pointerInParent - _pointerOffset);
        }

        private bool TryGetPointerInParent(PointerEventData eventData, out Vector2 localPoint) =>
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                RectTransform.parent as RectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localPoint);
    }
}
