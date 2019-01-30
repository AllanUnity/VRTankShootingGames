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
            if (instance == null)
            {
                Debug.Log("初始化" + typeof(T));
                GameObject games = new GameObject(typeof(T).Name);
                games.transform.SetParent(CSGame.Instance.transform);
                DontDestroyOnLoad(games);
                instance = games.AddComponent<T>() as T;
            }
            return instance;
        }
    }
    public virtual void Init()
    {

    }
}
