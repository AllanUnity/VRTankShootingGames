using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 按键输入管理类
/// </summary>
public class InputManager : CSMonoSingleton<InputManager>
{
    KeyCode[] keys;
    Dictionary<KeyCode, List<InputDelegate>> keysDelegate = new Dictionary<KeyCode, List<InputDelegate>>();
    public static void Add(KeyCode key, InputDelegate callback)
    {
        Singleton.AddCode(key, callback);
    }
    public static void Remove(KeyCode key)
    {
        Singleton.RemoveCode(key);
    }
    public static void Remove(KeyCode key, InputDelegate callback)
    {
        Singleton.RemoveCode(key, callback);
    }
    public override void Init()
    {
        base.Init();

        string[] keyNames = Enum.GetNames(typeof(KeyCode));
        int length = keyNames.Length;
        keys = new KeyCode[length];

        for (int i = 0; i < length; i++)
        {
            keys[i] = (KeyCode)Enum.Parse(typeof(KeyCode), keyNames[i]);
        }
    }


    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    public void AddCode(KeyCode key, InputDelegate callback)
    {
        List<InputDelegate> delegates = null;

        if (!keysDelegate.ContainsKey(key))
        {
            delegates = new List<InputDelegate>();
            keysDelegate.Add(key, delegates);
        }
        else
        {
            delegates = keysDelegate[key];
        }
        if (!delegates.Contains(callback))
        {
            delegates.Add(callback);
        }

    }
    public void RemoveCode(KeyCode key)
    {
        keysDelegate.Remove(key);
    }
    /// <summary>
    /// 移除事件
    /// </summary>
    /// <param name="key"></param>
    /// <param name="callback"></param>
    public void RemoveCode(KeyCode key, InputDelegate callback)
    {
        if (keysDelegate.ContainsKey(key))
        {
            keysDelegate[key].Remove(callback);
        }
    }
    private void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int j = 0; j < keys.Length; j++)
            {
                KeyCode item = keys[j];
                if (Input.GetKeyDown(item))
                {
                    if (keysDelegate != null && keysDelegate.ContainsKey(item) && keysDelegate[item] != null)
                    {
                        List<InputDelegate> delegates = keysDelegate[item];
                        for (int i = 0; i < delegates.Count; i++)
                        {
                            delegates[i]();
                        }
                    }
                }
            }
        }
    }
}
public delegate void InputDelegate();
