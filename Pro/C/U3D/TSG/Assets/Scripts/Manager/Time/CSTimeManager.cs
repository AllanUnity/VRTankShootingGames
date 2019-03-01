using System;
using System.Collections.Generic;

/// <summary>时间管理类</summary>
public class CSTimeManager : CSMonoSingleton<CSTimeManager>
{
    public delegate void CurrentTimeDelegate(DateTime dateTime);
    public event CurrentTimeDelegate GetCurrentTimeEvent;
    public override void Init()
    {
        base.Init();
    }
    List<IOnUpdate> updates = new List<IOnUpdate>();
    public void AddUpdate(IOnUpdate message)
    {
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i] == message)
            {
                return;
            }
        }
        updates.Add(message);
    }
    public void OnUpdate()
    {
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i] == null)
            {
                updates.RemoveAt(i);
                continue;
            }
            updates[i].OnUpdate();
        }
    }
    public void OnFixedUpdate()
    {
        GetCurrentTimeEvent(DateTime.Now);
        for (int i = 0; i < updates.Count; i++)
        {
            if (updates[i] == null)
            {
                updates.RemoveAt(i);
                continue;
            }
            updates[i].OnFixedUpdate();
        }
    }
}
