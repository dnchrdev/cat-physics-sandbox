using UnityEngine;

namespace Features.Player.Entities
{
    public readonly struct GroundProbe
    {
        public readonly bool IsGrounded;
        public readonly bool IsStable;
        public readonly Vector3 Normal;
        public readonly Vector3 AdjustmentVelocity;

        public GroundProbe(bool isGrounded, bool isStable, Vector3 normal, Vector3 adjustmentVelocity)
        {
            IsGrounded = isGrounded;
            IsStable = isStable;
            Normal = normal;
            AdjustmentVelocity = adjustmentVelocity;
        }

        public static GroundProbe None => new GroundProbe(false, false, Vector3.up, Vector3.zero);
        
        public GroundProbe AsUngrounded() => new GroundProbe(false, IsStable, Vector3.up, AdjustmentVelocity);
    }
}
