using UnityEngine;

namespace Features.Interaction.UseCases.Ports
{
    public interface IHand
    {
        Vector3 Position { get; }
        void SetHoldAnchor(Vector3 direction, float distance);
    }
}
