using UnityEngine;

namespace Features.Tutorial.UseCases.Ports
{
    public interface ITutorialFinishZoneProvider
    {
        Vector3 FinishAreaCenter { get; }
        float FinishAreaRadius { get; }
    }
}
