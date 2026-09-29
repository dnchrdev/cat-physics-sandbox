using UnityEngine;

namespace Features.Tutorial.Entities
{
    public static class TutorialZoneCalculator
    {
        public static bool IsInsideZone(Vector3 playerPosition, Vector3 zoneCenter, float zoneRadius)
        {
            var distance = (playerPosition - zoneCenter).magnitude;
            return distance < zoneRadius;
        }
    }
}
