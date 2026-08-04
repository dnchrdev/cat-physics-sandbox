using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Feature.UI
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public event Action<Vector2> OnValueChanged;

        public enum Axes { XAndY, XOnly, YOnly }

        [Header("Axes")]
        [SerializeField] private Axes activeAxes = Axes.XAndY;
        [SerializeField] private float valueMultiplier = 1f;

        [Header("References")]
        [SerializeField] private Image thumbImage;

        [Header("Radii")]
        [SerializeField] private float movementRadius = 75f;
        [SerializeField] private float deadzoneRadius = 0f;

        [Header("Value")]
        [SerializeField] private bool normalizeOutput = true;

        [Header("Dynamic mode")]
        [SerializeField] private bool isDynamic = false;
        [SerializeField] private RectTransform dynamicActivationArea;

        [Header("Follow pointer")]
        [SerializeField] private bool isFollowPointer = false;
        [SerializeField] private float followRadius = 75f;

        private RectTransform _joystickRect;
        private RectTransform _thumbRect;
        private Graphic _background;

        private Vector2 _anchoredOrigin;
        private bool _isHeld;
        private bool _isEnabled;
        private Vector2 _pointerLocalOrigin;
        private Vector2 _currentValue;

        private float _movementRadiusSq;
        private float _deadzoneRadiusSq;
        private float _invMovementRadius;
        private float _effectiveFollowRadiusSq;

        private PointerEventForwarder _forwarder;

        public Vector2 Value => _currentValue;
        public bool NormalizeOutput { get => normalizeOutput; set => normalizeOutput = value; }

        private void Update()
        {
            if (!_isEnabled) return;
            if (_isHeld) OnValueChanged?.Invoke(_currentValue);
        }

        private void OnDisable() => Release();

        public void UpdateValues(Vector2 pos, float radius, bool dynamic, bool follow)
        {
            EnsureRefs();
            _anchoredOrigin = pos;
            _joystickRect.anchoredPosition = _anchoredOrigin;
            UpdateValues(radius, dynamic, follow);
        }

        public void UpdateValues(float radius, bool dynamic, bool follow)
        {
            EnsureRefs();

            movementRadius = radius;
            followRadius = radius;
            isDynamic = dynamic;
            isFollowPointer = follow;

            _joystickRect.sizeDelta = new Vector2(radius * 2f, radius * 2f);

            if (_anchoredOrigin.sqrMagnitude < 0.1f)
                _anchoredOrigin = _joystickRect.anchoredPosition;

            CacheRadii();
            ResetThumb();
            TearDownCurrentMode();

            if (isDynamic) ConfigureDynamicMode();
            else ConfigureStaticMode();

            _isEnabled = true;
        }

        public void Disable()
        {
            _isEnabled = false;
            Release();
        }

        public void ForceRelease() => Release();

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!_isEnabled) return;

            _isHeld = true;
            _pointerLocalOrigin = Vector2.zero;

            if (isDynamic)
            {
                RectTransformUtility.ScreenPointToWorldPointInRectangle(
                    GetActivationArea(), eventData.position, eventData.pressEventCamera,
                    out var worldPos);

                _joystickRect.position = worldPos;
            }

            ProcessInput(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isEnabled) return;
            ProcessInput(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!_isEnabled) return;
            Release();
        }

        private void ProcessInput(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                _joystickRect, eventData.position, eventData.pressEventCamera,
                out var localPos);

            var rawDelta = ApplyAxisMask(localPos - _pointerLocalOrigin);

            if (rawDelta.sqrMagnitude <= _deadzoneRadiusSq)
            {
                _currentValue = Vector2.zero;
                _thumbRect.localPosition = Vector3.zero;
                return;
            }

            if (isFollowPointer && rawDelta.sqrMagnitude > _effectiveFollowRadiusSq)
            {
                var followClamped = rawDelta.normalized * followRadius;
                _joystickRect.localPosition += (Vector3)(rawDelta - followClamped);
            }

            var clampedDelta = rawDelta.sqrMagnitude > _movementRadiusSq
                ? rawDelta.normalized * movementRadius
                : rawDelta;

            _thumbRect.localPosition = (Vector3)clampedDelta;

            _currentValue = normalizeOutput
                ? clampedDelta.normalized * valueMultiplier
                : clampedDelta * _invMovementRadius * valueMultiplier;
        }

        private void EnsureRefs()
        {
            if (_joystickRect == null) _joystickRect = (RectTransform)transform;
            if (_thumbRect == null) _thumbRect = thumbImage.rectTransform;
            if (_background == null) _background = GetComponent<Graphic>();
        }

        private void CacheRadii()
        {
            _movementRadiusSq = movementRadius * movementRadius;
            _deadzoneRadiusSq = deadzoneRadius * deadzoneRadius;
            _invMovementRadius = movementRadius > 0f ? 1f / movementRadius : 0f;
            _effectiveFollowRadiusSq = followRadius * followRadius;
        }

        private void TearDownCurrentMode()
        {
            if (_forwarder != null)
            {
                Destroy(_forwarder);
                _forwarder = null;
            }

            SetRaycastTargets(false);
        }

        private void ConfigureStaticMode()
        {
            SetRaycastTargets(true);
            _joystickRect.anchoredPosition = _anchoredOrigin;
        }

        private void ConfigureDynamicMode()
        {
            SetRaycastTargets(false);
            EnsureDynamicActivationArea();

            _forwarder = GetActivationArea().gameObject.AddComponent<PointerEventForwarder>();
            _forwarder.Target = this;
        }

        private void EnsureDynamicActivationArea()
        {
            if (dynamicActivationArea != null) return;

            dynamicActivationArea = new GameObject(
                "JoystickActivationArea",
                typeof(RectTransform),
                typeof(CanvasRenderer),
                typeof(Image)
            ).GetComponent<RectTransform>();

            dynamicActivationArea.GetComponent<Image>().color = Color.clear;

            var canvas = thumbImage.canvas;
            dynamicActivationArea.SetParent(canvas.transform, false);
            dynamicActivationArea.SetAsFirstSibling();
            dynamicActivationArea.anchorMin = Vector2.zero;
            dynamicActivationArea.anchorMax = Vector2.one;
            dynamicActivationArea.sizeDelta = Vector2.zero;
            dynamicActivationArea.anchoredPosition = Vector2.zero;
        }

        private RectTransform GetActivationArea()
        {
            return dynamicActivationArea != null ? dynamicActivationArea : _joystickRect;
        }

        private void SetRaycastTargets(bool value)
        {
            if (_background != null) _background.raycastTarget = value;
            thumbImage.raycastTarget = value;
        }

        private Vector2 ApplyAxisMask(Vector2 v)
        {
            return activeAxes switch
            {
                Axes.XOnly => new Vector2(v.x, 0f),
                Axes.YOnly => new Vector2(0f, v.y),
                _ => v,
            };
        }

        private void Release()
        {
            _isHeld = false;
            _currentValue = Vector2.zero;

            ResetThumb();
            ResetPosition();

            OnValueChanged?.Invoke(Vector2.zero);
        }

        private void ResetThumb()
        {
            if (_thumbRect != null) _thumbRect.localPosition = Vector3.zero;
        }

        private void ResetPosition()
        {
            if (_joystickRect != null)
                _joystickRect.anchoredPosition = _anchoredOrigin;
        }
    }

    [RequireComponent(typeof(RectTransform))]
    internal sealed class PointerEventForwarder : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        internal Joystick Target { get; set; }

        public void OnPointerDown(PointerEventData e) => Target.OnPointerDown(e);
        public void OnDrag(PointerEventData e) => Target.OnDrag(e);
        public void OnPointerUp(PointerEventData e) => Target.OnPointerUp(e);
    }
}