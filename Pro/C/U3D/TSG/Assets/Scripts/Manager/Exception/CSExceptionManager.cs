using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class CSExceptionManager : CSMonoSingleton<CSExceptionManager>
{
    /// <summary>是否作为异常处理</summary>
    public bool IsHandler = false;
    /// <summary>当异常发生时是否退出程序</summary>
    public bool IsQuitWhenException = true;
    /// <summary>异常日志路径</summary>
    private string LogPath;
    /// <summary>Bug反馈程序的启动路径</summary>
    private string BugExePath;
    public override void Init()
    {
        base.Init();
        return;
        LogPath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/"));
        BugExePath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("/")) + "\\Bug.exe";

        if (IsHandler)
        {
            Application.logMessageReceived -= Handler;
            Application.logMessageReceived += Handler;
        }
    }
    private void OnDestroy()
    {
        Application.logMessageReceived -= Handler;
    }
    private void Handler(string condition, string stackTrace, LogType type)
    {
        string logPath = "";
        switch (type)
        {
            case LogType.Error:
            case LogType.Assert:
            case LogType.Exception:
                logPath = LogPath + "\\" + DateTime.Now.ToString("yyyy_MM_dd HH_mm_ss") + ".log";
                break;
            case LogType.Log:
                break;
            case LogType.Warning:
                break;
            default:
                break;
        }
        //打印日志
        if (Directory.Exists(LogPath))
        {
            File.AppendAllText(logPath, "[time]:" + DateTime.Now.ToString() + "\r\n");
            File.AppendAllText(logPath, "[type]:" + type.ToString() + "\r\n");
            File.AppendAllText(logPath, "[exception message]:" + condition + "\r\n"); File.AppendAllText(logPath, "[stack trace]:" + stackTrace + "\r\n");
        }
        //启动bug反馈程序
        if (File.Exists(BugExePath))
        {
            ProcessStartInfo pros = new ProcessStartInfo();
            pros.FileName = BugExePath;
            pros.Arguments = "\"" + logPath + "\"";
            Process pro = new Process();
            pro.StartInfo = pros; pro.Start();
        }
        //退出程序，bug反馈程序重启主程序
        if (IsQuitWhenException)
        {
            Application.Quit();
        }

    }
}
