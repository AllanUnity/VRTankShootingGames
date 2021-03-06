﻿using LitJson;
using UnityEngine;

/// <summary>工具类</summary>
public class Utility
{
    /// <summary>寻找子物体组件</summary>
    /// <typeparam name="T">组件类型</typeparam>
    /// <param name="parent">父物体</param>
    /// <param name="path">相对路径</param>
    /// <returns></returns>
    public static T GetObject<T>(Transform parent, string path) where T : UnityEngine.Object
    {
        if (parent == null) return null;

        Transform transform = parent.Find(path);
        if (transform == null) return null;

        if (typeof(T) == typeof(Transform)) return transform as T;

        if (typeof(T) == typeof(GameObject)) return transform.gameObject as T;

        return transform.GetComponent(typeof(T)) as T;
    }
    /// <summary>根据json翻译对象</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="json"></param>
    /// <returns></returns>
    public T JsonToObj<T>(string json) where T : Object
    {
        if (json != null)
        {
            try
            {
                T t = JsonMapper.ToObject<T>(json);
                return t;
            }
            catch { }
        }
        return null;
    }
    /// <summary>根据obj翻译json</summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public string ObjToJson(Object obj)
    {
        if (obj==null)
        {
            return null;
        }
        string str = JsonMapper.ToJson(obj);
        return str;
    }
    /// <summary>应用公司的名字</summary>
    public static string GetCompanyName()
    {
        return Application.companyName;
    }
    /// <summary>当前版本号</summary>
    public static string GetVersion()
    {
        return Application.version;
    }
    /// <summary>返回电量</summary>
    public static float GetBatteryLevel()
    {
        return SystemInfo.batteryLevel;
    }
    /// <summary>设备模型</summary>
    public static string GetDeviceModel()
    {
        return SystemInfo.deviceModel;
    }
    /// <summary>设备名称</summary>
    public static string GetDeviceName()
    {

        return SystemInfo.deviceName;
    }
    /// <summary>设备类型（PC电脑，掌上型）</summary>
    public static string GetDeviceType()
    {
        return SystemInfo.deviceType.ToString();
    }
    /// <summary>系统内存大小MB</summary>
    public static int GetMemorySize()
    {
        return SystemInfo.systemMemorySize;
    }
    /// <summary>操作系统</summary>
    public static string GetOperatingSystem()
    {
        return SystemInfo.operatingSystem;
    }
    /// <summary>设备唯一标识符</summary>
    public static string GetDeviceUniqueIdentifier()
    {
        return SystemInfo.deviceUniqueIdentifier;
    }
    /// <summary>显卡ID</summary>
    public static int GetGraphicsDeviceID()
    {
        return SystemInfo.graphicsDeviceID;
    }
    /// <summary>显卡名称</summary>
    public static string GetGraphicsDeviceName()
    {
        return SystemInfo.graphicsDeviceName;
    }
    /// <summary>显卡类型</summary>
    public static string GetGraphicsDeviceType()
    {
        return SystemInfo.graphicsDeviceType.ToString();
    }
    /// <summary>显卡供应商</summary>
    public static string GetGraphicsDeviceVendor()
    {
        return SystemInfo.graphicsDeviceVendor;
    }
    /// <summary>显卡供应唯一ID</summary>
    public static int GetGraphicsDeviceVendorID()
    {
        return SystemInfo.graphicsDeviceVendorID;
    }
    /// <summary>显卡版本号</summary>
    public static string GetGraphicsDeviceVersion()
    {
        return SystemInfo.graphicsDeviceVersion;
    }
    /// <summary>显存大小MB</summary>
    public static int GetGraphicsMemorySize()
    {
        return SystemInfo.graphicsMemorySize;
    }
    /// <summary>显卡是否支持多线程渲染</summary>
    public static bool GetGraphicsMultiThreaded()
    {
        return SystemInfo.graphicsMultiThreaded;
    }
    /// <summary>支持的渲染目标数量</summary>
    public static int GetsSupportedRenderTargetCount()
    {
        return SystemInfo.supportedRenderTargetCount;
    }
}
