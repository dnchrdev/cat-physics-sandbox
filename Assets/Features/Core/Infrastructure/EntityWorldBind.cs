using Features.Shared.Entities.Ports;
using UnityEngine;

namespace Features.Core.Infrastructure
{
    public class EntityWorldBind : MonoBehaviour
    {
        public IEntity AsEntity { get; private set; }
        public ITarget AsTarget { get; private set; }

        public void Bind(IEntity entity, ITarget target)
        {
            AsEntity = entity;
            AsTarget = target;
        }
    }
}
