using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UI管理类</summary>
public class UIManager : MonoBehaviour
{

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    /// <summary>打开展示的界面</summary>
    public List<UIBase> ShowPanels;
    /// <summary>隐藏的面板</summary>
    public List<UIBase> HidePanels;

    private void Init()
    {
        ShowPanels = new List<UIBase>();
        HidePanels = new List<UIBase>();
    }

    public void OpenPanel()
    {

    }
    public T GetPanel<T>() where T : UIBase
    {

        return null;
    }
    public void ClosePanel()
    {

    }
}
