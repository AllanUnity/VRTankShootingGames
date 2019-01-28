using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

}
