using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>继承自Mono的单例</summary>
/// <typeparam name="T"></typeparam>
public class CSMonoSingleton<T> : MonoBehaviour where T : UnityEngine.Component
{
    private static T instance = default(T);
    public static T Instance
    {
        get
        {
            return instance;
        }
    }
    public virtual void Init()
    {
        if (instance == null)
        {
            GameObject games = new GameObject(typeof(T).Name);
            DontDestroyOnLoad(games);
            instance = games.AddComponent<T>() as T;
        }
    }
}
