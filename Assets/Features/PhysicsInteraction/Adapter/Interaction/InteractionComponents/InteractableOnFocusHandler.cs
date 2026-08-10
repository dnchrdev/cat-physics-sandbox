using System.Collections;
using Feature.Shared;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public class InteractableOnFocusHandler
    {
        private readonly IInteractionTipShower _tipsShower;
        private readonly InteractionIndication _indication;
        private InteractionDetector _detector;
        
        private Interactable _focused;
        private Collider _trackedCollider;

        public Interactable Focused => _focused;
        public Collider TrackedCollider => _trackedCollider;
        
        public bool IsReadyToGrab => _focused != null || _trackedCollider != null;
        
        public InteractableOnFocusHandler(
            IInteractionTipShower tipsShower,
            InteractionIndication indication, 
            InteractionDetector detector)
        {
            _tipsShower = tipsShower;
            _indication = indication;
            _detector = detector; 
        }
        
        public void Tick(float dt)
        {
            var focusDetection = _detector.FocusDetect();
            
            if (!focusDetection.HasHit)
            {
                ClearFocus();
                return;
            }

            var isSameInteractable = _focused != null && _trackedCollider == focusDetection.Collider;
            
            if (isSameInteractable && _focused.UseGrabPositionIndication)
            {
                _indication.UpdateGrabIndicationPosition(focusDetection.ContactPoint);
                return;
            }

            ClearFocus();

            var interactable = focusDetection.Collider.GetComponent<Interactable>();
            if (interactable == null) return;

            _trackedCollider = focusDetection.Collider;
            _focused = interactable;

            _focused.ShowFocusVisual();
            _tipsShower.ShowHint(_focused.InteractionType);

            if (interactable.UseGrabPositionIndication) 
            { 
                _indication.ShowGrabIndication(focusDetection.ContactPoint);
                _indication.UpdateGrabIndicationPosition(focusDetection.ContactPoint);
                _indication.SwitchGrabIndicationImage(true);
            }
        }
        
        public void ClearFocus()
        {
            _indication.HideIndication();

            if (_focused == null && _trackedCollider == null) return;

            _focused.ResetVisual();
            _focused = null;
            _trackedCollider = null;
            _tipsShower.CloseAllHints();
        }

        public void Hit(float hitPower, IEntity player)
        {
            if(_focused == null)  return;
            
            var aimigPoint = _detector.GetHitAimingPoint();
            var direction = _detector.GetDirection(aimigPoint);

            _focused.Hit(direction, aimigPoint, hitPower, player);
        }
    }
}