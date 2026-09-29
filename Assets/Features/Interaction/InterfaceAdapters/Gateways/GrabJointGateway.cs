using Features.Interaction.Entities.Ports;
using UnityEngine;

namespace Features.Interaction.InterfaceAdapters.Gateways
{
    public sealed class GrabJointGateway : IGrabJoint
    {
        private const float PositionSpring = 350f;
        private const float PositionDamper = 35f;
        private const float MaximumForce = 350f;

        private readonly Rigidbody _body;
        private readonly Rigidbody _hand;
        private ConfigurableJoint _joint;

        public GrabJointGateway(Rigidbody body, Rigidbody hand)
        {
            _body = body;
            _hand = hand;
        }

        public void Attach(Vector3 localAnchor, bool drivesRotation)
        {
            if (_body == null) return;
            Detach();

            _joint = _body.gameObject.AddComponent<ConfigurableJoint>();
            _joint.autoConfigureConnectedAnchor = false;

            var drive = new JointDrive
            {
                positionSpring = PositionSpring,
                positionDamper = PositionDamper,
                maximumForce = MaximumForce
            };
            _joint.xDrive = drive;
            _joint.yDrive = drive;
            _joint.zDrive = drive;
            _joint.anchor = localAnchor;

            if (drivesRotation)
            {
                _joint.rotationDriveMode = RotationDriveMode.Slerp;
                _joint.slerpDrive = drive;
            }

            _joint.connectedBody = _hand;
        }

        public void MoveAnchor(Vector3 localAnchor)
        {
            if (_joint != null) _joint.anchor = localAnchor;
        }

        public void Detach()
        {
            if (_joint == null) return;
            Object.Destroy(_joint);
            _joint = null;
        }
    }
}
