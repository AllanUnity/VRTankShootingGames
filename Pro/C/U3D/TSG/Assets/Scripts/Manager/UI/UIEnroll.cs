using System;
using System.Collections.Generic;

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
        uiEnroll.Add(new Entry(typeof(UITEASER), "UITEASER", 0, "UI/view"));//健康游戏忠告
        uiEnroll.Add(new Entry(typeof(UITipsItem), "UITipsItem", 1001, "UI/view"));//提示
        uiEnroll.Add(new Entry(typeof(UIESCPanel), "UIESCPanel", 1002, "UI/view"));//Esc退出界面
        uiEnroll.Add(new Entry(typeof(UICombatMainPanel), "UICombatMainPanel", 401, "UI/view"));//战斗主面板
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
    /// <summary>
    /// 页面ID,
    /// Main场景中100-300
    /// Loading场景 300-400
    /// Combat场景 400+
    /// 可能在2个场景中出现的1000+
    /// Main之前的0-100
    /// </summary>
    public int id;
    public string path;
}
