using Features.Core.Infrastructure.Services;
using UnityEngine;

namespace Features.PhysicsInteraction.Adapter.Interaction.Grab
{
    public class GrabCenterWithRotationStrategy : GrabStrategyBase
    {
        public GrabCenterWithRotationStrategy(DestroyService ds) : base(ds) { }
        

        protected override void ConfigureJoint(
            ConfigurableJoint joint, JointDrive drive, Vector3 impactPosition)
        {
            joint.anchor = Vector3.zero;
            joint.rotationDriveMode = RotationDriveMode.Slerp;
            joint.slerpDrive = drive;
        }
            
    }
}
