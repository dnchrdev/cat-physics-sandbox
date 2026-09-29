using Features.Tutorial.UseCases.Ports;
using UnityEngine;

namespace Features.Tutorial.Infrastructure
{
    public sealed class TutorialRefs : MonoBehaviour, ITutorialFinishZoneProvider
    {
        [SerializeField] private Transform _finishAreaCenter;
        [SerializeField] private float _finishAreaRadius;

        public Vector3 FinishAreaCenter => _finishAreaCenter.position;
        public float FinishAreaRadius => _finishAreaRadius;
    }
}
