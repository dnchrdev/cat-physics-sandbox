using System;
using UnityEngine;
using ILogger = Features.Core.UseCases.Ports.ILogger;

namespace Features.Core.InterfaceAdapters.Services
{
    public sealed class UnityLogger : ILogger
    {
        public void Log(Type context, string message)
        {
            Debug.Log($"{context.Name}: {message}");
        }

        public void LogWarning(Type context, string message)
        {
            Debug.LogWarning($"{context.Name}: {message}");
        }

        public void LogError(Type context, string message)
        {
            Debug.LogError($"{context.Name}: {message}");
        }
    }
}
