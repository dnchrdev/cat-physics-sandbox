using Features.Core.UseCases.Ports;
using UnityEngine;

namespace Features.Core.InterfaceAdapters.Services
{
    public sealed class UnityTimeProvider : ITimeProvider
    {
        public void SetTimeScale(float scale)
        {
            Time.timeScale = scale;
        }
    }
}
