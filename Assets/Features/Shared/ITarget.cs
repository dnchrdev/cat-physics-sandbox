using Features.Core.Utils;

namespace Features.Shared
{
    public interface ITarget
    {
        Result RecieveHit(AttackInfo attackInfo);
    }
}