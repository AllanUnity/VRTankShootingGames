using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 设置面板
/// </summary>
public class UISettingPanel : UIBase
{
    public Button CloseBtn;
    public override void Init()
    {
        base.Init();
        AddButtonClickEvent(CloseBtn, ClosePanel);
    }

    private void ClosePanel()
    {
        UIManager.Singleton.ClosePanel<UISettingPanel>();
    }

    public override void Show()
    {
        base.Show();
    }
}
