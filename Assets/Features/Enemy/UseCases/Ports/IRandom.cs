namespace Features.Enemy.UseCases.Ports
{
    public interface IRandom
    {
        float Range(float minInclusive, float maxInclusive);

        int Range(int minInclusive, int maxExclusive);
    }
}
