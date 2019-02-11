using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>继承自Mono的单例</summary>
/// <typeparam name="T"></typeparam>
public class CSMonoSingleton<T> : MonoBehaviour where T : CSMonoSingleton<T>
{
    private static T instance = default(T);
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    /// <summary>初始化</summary>
    /// <param name="parent"></param>
    public static void Initialize(Transform parent)
    {
        if (instance != default(T))
        {
            return;
        }
        GameObject go = new GameObject(typeof(T).Name);
        if (parent != null)
        {
            go.transform.SetParent(parent);
        }
        instance = go.AddComponent<T>();
        instance.Init();
    }

    /// <summary>是否销毁</summary>
    public virtual bool IsDonnotDestroy { get { return false; } }

    public virtual void Init() { }
    public virtual void Destroy()
    {
        if (gameObject!=null)
        {
            if (!IsDonnotDestroy)
            {
                UnityEngine.Object.Destroy(gameObject);
                instance = null;
            }
        }
    }

}
