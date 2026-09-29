using System;
using System.Collections.Generic;
using Features.Shared.Entities;

namespace Features.Storage.Entities.Ports
{
    public interface IButtonVisibilityReader
    {
        event Action<AdjustableButtonType, bool> Changed;
        bool GetVisible(AdjustableButtonType type);
        IReadOnlyDictionary<AdjustableButtonType, bool> GetAll();
    }
}
