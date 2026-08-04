using System;
using System.IO;
using UnityEngine;

public static class FileLogger
{
    private static string _logPath;
    private static StreamWriter _writer;
    private static bool _initialized;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        if (_initialized)
            return;

        _initialized = true;

        _logPath = Path.Combine(Application.persistentDataPath, "game_log.txt");

        _writer = new StreamWriter(_logPath, true)
        {
            AutoFlush = true
        };

        //Application.logMessageReceived += HandleUnityLog;
    }

    public static void Log(string message)
    {
        Debug.Log(message);
    }

    private static void HandleUnityLog(
        string condition,
        string stackTrace,
        LogType type)
    {
        if (_writer == null)
            return;
        //{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{type}] 
        string log =
            $"[{condition}";

        if (type == LogType.Error ||
            type == LogType.Exception ||
            type == LogType.Assert)
        {
            log += $"\n{stackTrace}";
        }

        _writer.WriteLine(log);
    }
}