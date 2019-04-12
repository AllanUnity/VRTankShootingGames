using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug
{
    public static bool developerConsoleVisible;
    public static void Log(object obj)
    {
        UnityEngine.Debug.Log(obj);
    }
    public static void LogColor(object obj)
    {
        string str = "<color=yellow>" + obj.ToString() + "</color>";
        Log(str);
    }
    public static void LogError(object obj)
    {
        UnityEngine.Debug.LogError(obj);
    }
    public static void LogWarning(object obj)
    {
        UnityEngine.Debug.LogWarning(obj);
    }
    public static void LogException(System.Exception obj)
    {
        UnityEngine.Debug.LogException(obj);
    }
}
