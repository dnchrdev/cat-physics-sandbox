using UnityEngine;

namespace Features.Enemy.Entities.Calculators
{
    public static class RespawnWaitTimeCalculator
    {
        public static float CalculateRemainingWaitTime(float respawnDuration, float timerSinceLastInteraction)
        {
            return Mathf.Clamp(respawnDuration - timerSinceLastInteraction, 0f, respawnDuration);
        }
    }
}
