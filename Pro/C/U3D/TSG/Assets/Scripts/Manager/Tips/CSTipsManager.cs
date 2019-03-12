using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>提示器</summary>
public class CSTipsManager : CSMonoSingleton<CSTipsManager>,IOnUpdate
{
    public delegate void DelegateOpenTipsCallBack(UITipsItem ui);

    public List<UITipsItem> hidePool = new List<UITipsItem>();
    public List<UITipsItem> showPool = new List<UITipsItem>();
    public override void Init()
    {
        base.Init();
        CSTimeManager.Singleton.AddUpdate(this);

    }
    private int number=600;
    public void AddTips(DelegateOpenTipsCallBack cb)
    {
        if (hidePool.Count == 0)
        {
            UIManager.Singleton.OpenPanel<UITipsItem>(number, (ui) =>
            {
                UITipsItem tipsItem = (UITipsItem)ui;
                showPool.Add(tipsItem);
                cb(tipsItem);
            });
            number++;
        }
        else
        {
            UITipsItem tipsItem = hidePool[0];
            hidePool.RemoveAt(0);
            tipsItem.Show();
            tipsItem.gameObject.SetActive(true);
            showPool.Add(tipsItem);
            cb(tipsItem);
        }
    }

    public void Hide(UITipsItem uITipsItem)
    {
        //Debug.Log()
        if (showPool.Contains(uITipsItem))
        {
            uITipsItem.Hide();
            showPool.Remove(uITipsItem);
            hidePool.Add(uITipsItem);
        }
    }

    public void OnFixedUpdate()
    {
    }

    public void OnUpdate(float time)
    {
        for (int i = 0; i < showPool.Count; i++)
        {
            showPool[i].OnUpdate(time);
        }
    }
}
