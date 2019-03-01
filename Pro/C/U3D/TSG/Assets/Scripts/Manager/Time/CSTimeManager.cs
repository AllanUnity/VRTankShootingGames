using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>时间管理类</summary>
public class CSTimeManager : CSMonoSingleton<CSTimeManager>
{
    public delegate void CurrentTimeDelegate(DateTime dateTime);
    public event CurrentTimeDelegate GetCurrentTimeEvent;
    public override void Init()
    {
        base.Init();

    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        GetCurrentTimeEvent(DateTime.Now);
    }
}
