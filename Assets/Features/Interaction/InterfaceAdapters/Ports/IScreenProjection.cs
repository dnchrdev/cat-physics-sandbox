using UnityEngine;

namespace Features.Interaction.InterfaceAdapters.Ports
{
    public interface IScreenProjection
    {
        bool IsFacingPoint(Vector3 worldPosition);
        Vector3 WorldToScreenPoint(Vector3 worldPosition);
    }
}
