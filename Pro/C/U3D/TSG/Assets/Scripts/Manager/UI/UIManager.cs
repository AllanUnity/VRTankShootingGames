using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour {

    private static UIManager instance;

    public static UIManager Instance
    {
        get
        {
            return instance;
        }
    }

    public string CanvasName = "UI/UIMainCanvas";
    private Transform canvas;
    /// <summary>打开展示的界面</summary>
    public List<UIBase> ShowPanels;
    /// <summary>隐藏的面板</summary>
    public List<UIBase> HidePanels;

    private void Init()
    {
        ShowPanels = new List<UIBase>();
        HidePanels = new List<UIBase>();
    }
    public Transform GetUICanvas()
    {
        if (canvas==null)
        {
            GameObject _canvas = GameObject.Find(CanvasName);
            if (_canvas!=null)
            {
                canvas = _canvas.transform;
            }
        }
        return canvas;
    }
    public void OpenPanel()
    {

    }
    public void ClosePanel()
    {

    }
}
