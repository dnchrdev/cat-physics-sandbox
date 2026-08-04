using System;
using UnityEngine;

public class UnityLogger : ILogger
{
    public void Log(Type logType, string message)
    {
        Debug.Log($"{nameof(logType)}: {message}");
    }

    public void LogWarning(Type logType, string message)
    {
        Debug.LogWarning($"{nameof(logType)}: {message}");
    }

    public void LogError(Type logType, string message)
    {
        Debug.LogError($"{nameof(logType)}: {message}");
    }
}
