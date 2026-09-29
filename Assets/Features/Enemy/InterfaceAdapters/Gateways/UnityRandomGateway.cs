using Features.Enemy.UseCases.Ports;

namespace Features.Enemy.InterfaceAdapters.Gateways
{
    public sealed class UnityRandomGateway : IRandom
    {
        public float Range(float minInclusive, float maxInclusive) =>
            UnityEngine.Random.Range(minInclusive, maxInclusive);

        public int Range(int minInclusive, int maxExclusive) =>
            UnityEngine.Random.Range(minInclusive, maxExclusive);
    }
}
