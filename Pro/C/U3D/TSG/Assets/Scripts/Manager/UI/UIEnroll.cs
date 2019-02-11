using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UI注册机</summary>
public class UIEnroll 
{
    private static UIEnroll instance;
    public static UIEnroll Instance
    {
        get
        {
            if (instance==null)
            {
                instance = new UIEnroll();
            }
            return instance;
        }
    }
    private UIEnroll()
    {
        Init();
    }
    
    private List<Entry> uiEnroll = new List<Entry>();
    public Entry FindEntry(string typeName)
    {
        return uiEnroll.Find(f => f.typeName == typeName);
    }
    private void Init()
    {

    }
   
}
[Serializable]
public class Entry
{
    public Type type;
    public string typeName;
    public int id;
    public string path;
}
