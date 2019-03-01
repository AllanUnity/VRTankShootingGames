using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>时间管理类</summary>
public class CSTimeManager : CSMonoSingleton<CSTimeManager>, IOnUpdate
{
    public delegate void CurrentTimeDelegate(DateTime dateTime);
    public event CurrentTimeDelegate GetCurrentTimeEvent;
    public override void Init()
    {
        base.Init();
        CSGame.Instance.AddUpdate(this);
    }

    public void OnUpdate()
    {
        GetCurrentTimeEvent(DateTime.Now);
    }
}
