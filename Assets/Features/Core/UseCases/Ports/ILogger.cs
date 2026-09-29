using System;

namespace Features.Core.UseCases.Ports
{
    public interface ILogger
    {
        void Log(Type logType, string message);
        void LogWarning(Type logType, string message);
        void LogError(Type logType, string message);
    }
}
