using Feature.Core;
using System.Collections;
using Feature.CameraFeature;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace Feature.PhysicsInteraction
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
