using Features.Shared.Utilities;

namespace Features.Shared.Entities.Ports
{
    public interface ITarget
    {
        Result ReceiveHit(AttackInfo attackInfo);
    }
}
