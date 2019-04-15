using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>按ESC弹出的界面</summary>
public class UIESCPanel : UIBase
{
    public Button closeGameBtn;
    public Button settingBtn;
    public override void Init()
    {
        base.Init();
        AddButtonClickEvent(closeGameBtn, CloseGameOnClick);
        AddButtonClickEvent(settingBtn, OpenSettingPanel);
    }
    /// <summary>打开设置界面</summary>
    private void OpenSettingPanel()
    {
        //UIManager.Singleton.ClosePanel<UIESCPanel>();
        UIManager.Singleton.OpenPanel<UISettingPanel>(); 
    }
    /// <summary>退出游戏</summary>
    private void CloseGameOnClick()
    {
        CSGame.Instance.Quit();
    }
}
