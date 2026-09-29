using UnityEngine;

namespace Features.Shared.Entities.Ports
{
 public interface IReadOnlyPlayer : IEntity
    {
        Vector3 Position { get; }
    }
}
