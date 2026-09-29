using UnityEngine;

namespace Features.Interaction.Entities.Ports
{
  public interface IGrabJoint
    {
        void Attach(Vector3 localAnchor, bool drivesRotation);
        void MoveAnchor(Vector3 localAnchor);
        void Detach();
    }
}
