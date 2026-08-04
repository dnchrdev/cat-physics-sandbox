using System;
using Feature.Core;
using Feature.Shared;
using UnityEngine;

namespace Feature.PlayerFeature
{
    public class Player : IEntity, ITarget, IReadOnlyPlayer, ILiveEvents
    {
        private IReadOnlyCharacterMotor _readOnlyMotor;

        public bool IsAlive { get; private set; }
        public Vector3 Position => _readOnlyMotor.GetPosition();

        public TeamType Team => TeamType.Player;

        public event Action<AttackInfo> HitRecieved;
        public event Action Continiued;
        public event Action Respawned;
        public event Action Knockouted;

        public Player(IReadOnlyCharacterMotor readOnlyMotor)
        {
            _readOnlyMotor = readOnlyMotor;
            IsAlive = true;
        }

        public Result RecieveHit(AttackInfo attackInfo)
        {
            if (!IsAlive)
                return Result.Failure("Player is already died");

            if (attackInfo.Entity.Team == TeamType.None || attackInfo.Entity.Team == Team)
                return Result.Failure("Same team");

            Die();

            HitRecieved?.Invoke(attackInfo);

            return Result.Success();
        }

        public Result Die()
        {
            if (!IsAlive)
                return Result.Failure("Player is already dead.");

            IsAlive = false;
            Knockouted?.Invoke();
            return Result.Success();
        }

        public Result Respawn()
        {
            if (IsAlive)
                return Result.Failure("Player is not dead and cannot respawn.");

            IsAlive = true;
            Respawned?.Invoke();
            return Result.Success();
        }
        
        public Result Continue()
        {
            if (IsAlive)
                return Result.Failure("Player is not dead and cannot respawn.");
            
            IsAlive = true;
            
            Continiued?.Invoke();
            return Result.Success();
        }
    }
}