using Features.Quests.InterfaceAdapters.Ports;
using UnityEngine;

namespace Features.Quests.Infrastructure.Targets
{
    public sealed class TransformHintAnchor : IHintAnchor
    {
        private readonly Transform _transform;

        public TransformHintAnchor(Transform transform)
        {
            _transform = transform;
        }

        public Vector3 WorldPosition => _transform.position;
    }
}
