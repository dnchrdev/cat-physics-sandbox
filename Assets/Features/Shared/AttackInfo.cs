using UnityEngine;

namespace Feature.Shared
{
    public struct AttackInfo
    {
        public IEntity Entity;
        public bool IsTrigger;
        public float HitVelocity;

        public AttackInfo(IEntity entity, bool isTrigger, float hitVelocity = 0f)
        {
            Entity = entity;
            IsTrigger = isTrigger;
            HitVelocity = hitVelocity;
        }
    }
}