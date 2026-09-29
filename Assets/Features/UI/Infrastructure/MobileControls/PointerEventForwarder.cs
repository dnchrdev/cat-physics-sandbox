using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.UI.Infrastructure.MobileControls
{
    [RequireComponent(typeof(RectTransform))]
    class PointerEventForwarder : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        internal Joystick Target { get; set; }

        public void OnPointerDown(PointerEventData e) => Target.OnPointerDown(e);
        public void OnDrag(PointerEventData e) => Target.OnDrag(e);
        public void OnPointerUp(PointerEventData e) => Target.OnPointerUp(e);
    }
}
