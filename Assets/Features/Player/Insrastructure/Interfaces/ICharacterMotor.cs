using UnityEngine;

namespace Feature.PlayerFeature
{
    public interface ICharacterMotor
    {
        public void CheckForGround();
        public bool IsGrounded();
        public bool IsStable();
        public Vector3 GetVelocity();

        public Vector3 GetPosition();
        public void SetPosition(Vector3 newPos);
        public void SetVelocity(Vector3 velocity);
        public void SetForcedGrounded(bool groounded);
        public void SetExtendSensorRange(bool _isExtended);

        public Vector3 GetGroundNormal();
        public Collider GetGroundCollider();
        public Vector3 GetGroundAdjustmentVelocity();
    }
}