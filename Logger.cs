using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Logger : MonoBehaviour
{
    private static Logger m_Instance;
    public static Logger Instance { get { return m_Instance != null ? m_Instance : m_Instance = FindObjectOfType<Logger>(); } }

    private List<Log> m_LogList = new List<Log>();
    public List<Log> LogList { get { return m_LogList; } }

    public void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        m_LogList.Add(new Log() { Type = type, Message = logString, StackTrace = stackTrace });
    }

    public IEnumerable<Log> GetLogByType(params LogType[] types)
    {
        return m_LogList.Where(x => types.Contains(x.Type));
    }
}

[Serializable]
public class Log
{
    public LogType Type;
    public string Message;
    public string StackTrace;
    public override string ToString() => $"{Type.ToString()} : {Message}\n{StackTrace}";
}