using System.Collections;
using UnityEngine;

namespace Feature.Core
{
    public static class AdditionalMath
    {
        public static float Map(float value, float inMin, float inMax, float outMin, float outMax)
        {
            return (value - inMin) / (inMax - inMin) * (outMax - outMin) + outMin;
        }
    }
}