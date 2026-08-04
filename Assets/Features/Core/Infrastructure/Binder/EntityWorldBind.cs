using Feature.EnemyFeature;
using Feature.Shared;
using UnityEngine;

namespace Feature.Core
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