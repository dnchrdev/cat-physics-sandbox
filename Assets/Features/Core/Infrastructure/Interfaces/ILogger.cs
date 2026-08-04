
using System;

public interface ILogger
{
    void Log(Type logType, string message);
    void LogWarning(Type logType, string message);
    void LogError(Type logType, string message);
}
