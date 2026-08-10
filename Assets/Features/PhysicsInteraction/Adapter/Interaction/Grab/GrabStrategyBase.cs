using Feature.Core;
using System.Collections;
using Feature.CameraFeature;
using Unity.VisualScripting;
using UnityEngine;

namespace Feature.PhysicsInteraction
{
    public abstract class GrabStrategyBase : IGrabable
    {
        protected readonly DestroyService DestroyService;
        protected ConfigurableJoint Joint;
        protected Transform Transform;
        protected Collider Collider;

        private bool _isGrabbed;

        public bool IsGrabbed() => _isGrabbed;

        protected GrabStrategyBase(DestroyService destroyService)
        {
            DestroyService = destroyService;
        }

        public bool Grab(Rigidbody hand, Rigidbody rb, Collider collider, Vector3 grabPosition)
        {
            if (_isGrabbed) return false;

            rb.isKinematic = false;
            Collider = collider;
            Transform = rb.transform;
            Joint = rb.AddComponent<ConfigurableJoint>();
            Joint.autoConfigureConnectedAnchor = false;

            var drive = new JointDrive
            {
                positionSpring = 350f,
                positionDamper = 35f,
                maximumForce = 350f
            };

            Joint.xDrive = drive;
            Joint.yDrive = drive;
            Joint.zDrive = drive;

            ConfigureJoint(Joint, drive, grabPosition);

            Joint.connectedBody = hand;
            _isGrabbed = true;
            return true;
        }

        public virtual Vector3 GetGrabPosition(Vector3 handPosition, IReadOnlyCamera camera, LayerMask interactionMask)
        {
            return Transform.position;
        }

        public virtual void UpdateAnchor(Vector3 grabPosition)
        {
            
        }

        public void Throw(Vector3 toPoint, float power, Rigidbody rb)
        {
            Release();
            rb.AddForce((toPoint - rb.position).normalized * power, ForceMode.Impulse);
        }

        public void Release()
        {
            if (Joint != null)
                DestroyService.Destroy(Joint);
            _isGrabbed = false;
        }

        protected abstract void ConfigureJoint(ConfigurableJoint joint, JointDrive drive, Vector3 impactPosition);
        
    }
}