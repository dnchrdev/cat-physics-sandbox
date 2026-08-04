using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Feature.UI
{
    public class DeltaDragPanel : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Vector2> GetValueEvent;

        [SerializeField] private RectTransform touchArea;

        private Vector2 m_previousPointerPos;
        private Vector2 m_deltaDrag = Vector2.zero;
        private bool m_dragging = false;

        private void Start()
        {
        }

        private void Update()
        {
            if (!m_dragging)
                m_deltaDrag = Vector2.zero;
            m_dragging = false;

            GetValueEvent?.Invoke(m_deltaDrag);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                touchArea, eventData.position, eventData.pressEventCamera, out m_previousPointerPos);
        }

        public void OnDrag(PointerEventData eventData)
        {
            m_dragging = true;

            Vector2 pointerPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                touchArea, eventData.position, eventData.pressEventCamera, out pointerPos);

            m_deltaDrag = (pointerPos - m_previousPointerPos);
            m_previousPointerPos = pointerPos;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            m_dragging = false;
            m_deltaDrag = Vector2.zero;
        }
    }
}