using System;
using Features.Shared.Entities;
using Features.Shared.Entities.Ports;
using Features.Shared.Utilities;
using UnityEngine;

namespace Features.Player.Entities
{
    public class Player : IPlayerTarget, IReadOnlyPlayer, IPlayerLifeEvents
    {
        public bool IsAlive { get; private set; }
        public Vector3 Position { get; private set; }
        public TeamType Team => TeamType.Player;

        public event Action<AttackInfo> HitReceived;
        public event Action Continued;
        public event Action Respawned;
        public event Action Knockedout;

        public Player()
        {
            IsAlive = true;
        }

        public void UpdatePosition(Vector3 position)
        {
            Position = position;
        }

        public Result ReceiveHit(AttackInfo attackInfo)
        {
            if (!IsAlive)
                return Result.Failure("Player is already dead.");

            if (attackInfo.Entity.Team == TeamType.None || attackInfo.Entity.Team == Team)
                return Result.Failure("Same team");

            Die();
            HitReceived?.Invoke(attackInfo);
            return Result.Success();
        }

        private Result Die()
        {
            if (!IsAlive)
                return Result.Failure("Player is already dead.");

            IsAlive = false;
            Knockedout?.Invoke();
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
                return Result.Failure("Player is not dead and cannot continue.");

            IsAlive = true;
            Continued?.Invoke();
            return Result.Success();
        }
    }
}
