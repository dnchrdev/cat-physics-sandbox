using Feature.Core;
using Feature.Shared;
using System;
using UnityEngine;

namespace Feature.EnemyFeature
{
    public class Enemy : IEntity, ITarget, ILiveEvents
    {
        public event Action<AttackInfo> HitRecieved;
        public event Action Continiued;
        public event Action Knockouted;
        
        private Blackboard _bb;

        public TeamType Team => TeamType.Enemy;

        public bool IsAlive => _bb.IsRagdoll == false;
        
        public IEntity TargetPlayer { get; private set; }

        public Enemy(Blackboard bb)
        {
            _bb = bb;
        }

        public Result RecieveHit(AttackInfo attackInfo)
        {
            if (!IsAlive)
                return Result.Failure("Enemy is already died");

            if (attackInfo.Entity.Team == TeamType.None || attackInfo.Entity.Team == Team)
                return Result.Failure("Same team");

            //Debug.Log("Damage Recieved");

            TargetPlayer = attackInfo.Entity;
            
            HitRecieved?.Invoke(attackInfo);
            return Result.Success();
        }

        public void Knockout()
        {
            Knockouted?.Invoke();
        }

        public Result Respawn()
        {
            Continiued?.Invoke();
            return Result.Success();
        }
    }
}