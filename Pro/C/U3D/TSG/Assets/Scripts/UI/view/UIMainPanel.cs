using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainPanel : UIBase
{
    public Button settingBtn;
    public override void Init()
    {
        base.Init();
        AddButtonClickEvent(settingBtn, OpenSettingWindows);
    }

    private void OpenSettingWindows()
    {
        UIManager.Singleton.OpenPanel<UISettingPanel>();
    }
}
