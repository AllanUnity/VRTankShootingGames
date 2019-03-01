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
        uiEnroll.Add(new Entry(typeof(UITankController), "UITankController", 0, "UI/view"));
        uiEnroll.Add(new Entry(typeof(UIShowSight), "UIShowSight", 1, "UI/view"));
        uiEnroll.Add(new Entry(typeof(UICombatMainPanel), "UICombatMainPanel", 301, "UI/view"));
    }
   
}
[Serializable]
public class Entry
{
    public Entry(Type type,string typeName,int id,string path)
    {
        this.type = type;
        this.typeName = typeName;
        this.id = id;
        this.path = path;
    }
    public Type type;
    public string typeName;
    public int id;
    public string path;
}
