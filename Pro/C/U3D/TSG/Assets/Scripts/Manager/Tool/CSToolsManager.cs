using System;
using System.Collections;
using UnityEngine;

/// <summary>工具管理类</summary>
public class CSToolsManager : CSMonoSingleton<CSToolsManager>
{
    public override void Init()
    {
        base.Init();
    }

    /// <summary>
    /// 携程统一调用该位置启动
    /// </summary>
    /// <param name="customIemtor"></param>
    public Coroutine StartCustomCoroutine(Func<object[], IEnumerator> ienum, params object[] objs)
    {
        return StartCoroutine(ienum(objs));

    }
    public Coroutine StartCustomCoroutine(IEnumerator ienum)
    {
        return StartCoroutine(ienum);
    }

    /// <summary>获取屏幕截图</summary>
    /// <param name="captureName"></param>
    public void GetCaptureScreenshot(string captureName)
    {
        UnityEngine.ScreenCapture.CaptureScreenshot(captureName);
    }
}
