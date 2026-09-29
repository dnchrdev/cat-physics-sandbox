using UnityEngine;

namespace Features.Enemy.InterfaceAdapters.Ports
{
    public interface IHeadLookView
    {
        void SetLookAt(Vector3 target, float weight);
    }
}
