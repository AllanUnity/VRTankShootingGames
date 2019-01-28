using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsManager : MonoBehaviour {

    private static ToolsManager instance;

    public static ToolsManager Instance
    {
        get
        {
            return instance;
        }
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
}
