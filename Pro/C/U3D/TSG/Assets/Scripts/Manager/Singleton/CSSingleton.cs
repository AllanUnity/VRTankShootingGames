using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>不继承Mono的单例</summary>
public class CSSingleton<T>where T:class, new()
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new T();
            }
            return instance;
        }
    }    
}
