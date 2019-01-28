/*
 事件管理器
 创建时间: 2019.1.28
 作用:事件管理,1,通用型的在EventSystemManger中操作,2,针对性的在特殊类中创建EventHanlderManager对象并发送
 */
using System.Collections.Generic;
using UnityEngine;

public delegate void DelegateHanlderCallBack(uint eventId, params object[] objs);

/// <summary>通用事件管理器</summary>
public class EventSystemManager
{
    static BaseEvent mDispatcher = new BaseEvent();

    public static void AddEvent(uint msgId,DelegateHanlderCallBack cb)
    {
        mDispatcher.AddEvent(msgId, cb);
    }
    public void RemoveEvent(uint msgId, DelegateHanlderCallBack cb)
    {
        mDispatcher.RemoveEvent(msgId, cb);
    }
    public void RemoveEvent(uint msgId)
    {
        mDispatcher.RemoveEvent(msgId);
    }
    public void ClearEvent()
    {
        mDispatcher.ClearEvent();
    }
    public void SendEvent(uint uiEvtID, params object[] objData)
    {
        mDispatcher.SendEvent(uiEvtID, objData);
    }
}
//针对性
public class EventHanlderManager
{
    BaseEvent mDispatcher = null;
    public EventHanlderManager()
    {
        mDispatcher = new BaseEvent();
    }
    /// <summary>添加事件</summary>
    /// <param name="msgId"></param>
    /// <param name="cb"></param>
    public void AddEvent(uint msgId, DelegateHanlderCallBack cb)
    {
        mDispatcher.AddEvent(msgId, cb);
    }
    /// <summary>移出指定事件</summary>
    /// <param name="msgId"></param>
    /// <param name="cb"></param>
    public void RemoveEvent(uint msgId, DelegateHanlderCallBack cb)
    {
        mDispatcher.RemoveEvent(msgId, cb);
    }
    /// <summary>移出事件</summary>
    /// <param name="msgId"></param>
    public void RemoveEvent(uint msgId)
    {
        mDispatcher.RemoveEvent(msgId);
    }
    public void ClearEvent()
    {
        mDispatcher.ClearEvent();
    }
    /// <summary>发送事件</summary>
    /// <param name="uiEvtID"></param>
    /// <param name="objData"></param>
    public void SendEvent(uint uiEvtID, params object[] objData)
    {
        mDispatcher.SendEvent(uiEvtID, objData);
    }

}

/// <summary>事件委托类</summary>
public class EventDelegate
{
    List<DelegateHanlderCallBack> arrCallBack = new List<DelegateHanlderCallBack>();
    List<DelegateHanlderCallBack> arr2Process = new List<DelegateHanlderCallBack>();

    private uint uiEvtID;
    /// <summary>初始化</summary>
    /// <param name="_uiEvtId"></param>
    public EventDelegate(uint _uiEvtId)
    {
        uiEvtID = _uiEvtId;
    }
    /// <summary>增加事件</summary>
    /// <param name="cb"></param>
    public void AddCallBack(DelegateHanlderCallBack cb)
    {
        if (!arrCallBack.Contains(cb))
        {
            arrCallBack.Add(cb);
        }
        else
        {
            if (Debug.developerConsoleVisible)
            {
                Debug.Log("AddCallBack Same");
            }
        }
    }
    /// <summary>移除事件</summary>
    /// <param name="cb"></param>
    public void RemoveCallBack(DelegateHanlderCallBack cb)
    {
        arrCallBack.Remove(cb);
    }
    /// <summary>清空事件</summary>
    public void ClearCallBack()
    {
        arrCallBack.Clear();
    }

    /// <summary>发送事件</summary>
    /// <param name="objData"></param>
    public void SendEvent(params object[] objData)
    {
        arr2Process.AddRange(arrCallBack);//拷贝所有元素

        for (int i = 0; i < arr2Process.Count; i++)
        {
            DelegateHanlderCallBack cb = arr2Process[i];
            cb(uiEvtID, objData);
        }
        arr2Process.Clear();
    }
    /// <summary>获取事件长度</summary>
    /// <returns></returns>
    public int GetCallBackLength()
    {
        return arrCallBack.Count;
    }
}
/// <summary>基础事件</summary>
public class BaseEvent
{
    /// <summary>事件集合</summary>
    public Dictionary<uint, EventDelegate> mDicEvtDelegate = new Dictionary<uint, EventDelegate>();

    /// <summary>添加事件</summary>
    /// <param name="uiEvtID"></param>
    /// <param name="callBack"></param>
    public void AddEvent(uint uiEvtID, DelegateHanlderCallBack callBack)
    {
        EventDelegate evtDelegate = null;
        if (!mDicEvtDelegate.ContainsKey(uiEvtID))
        {
            evtDelegate = new EventDelegate(uiEvtID);
            mDicEvtDelegate.Add(uiEvtID, evtDelegate);
        }
        else
        {
            evtDelegate = mDicEvtDelegate[uiEvtID];
        }
        if (null != evtDelegate)
        {
            evtDelegate.AddCallBack(callBack);
        }
    }

    /// <summary>删除事件</summary>
    /// <param name="uiEvtID"></param>
    /// <param name="callBack"></param>
    public void RemoveEvent(uint uiEvtID, DelegateHanlderCallBack callBack)
    {
        if (mDicEvtDelegate.ContainsKey(uiEvtID))
        {
            EventDelegate evtDelegate = mDicEvtDelegate[uiEvtID];
            evtDelegate.RemoveCallBack(callBack);
            if (evtDelegate.GetCallBackLength() == 0)
            {
                RemoveEvent(uiEvtID);
            }
        }
    }

    /// <summary>删除事件</summary>
    /// <param name="uiEvtId"></param>
    public void RemoveEvent(uint uiEvtId)
    {
        if (mDicEvtDelegate.ContainsKey(uiEvtId))
        {
            mDicEvtDelegate.Remove(uiEvtId);
        }
    }

    /// <summary>分发事件</summary>
    /// <param name="uiEvtID"></param>
    /// <param name="objData"></param>
    /// <returns></returns>
    public void SendEvent(uint uiEvtID, params object[] objData)
    {
        if (mDicEvtDelegate.ContainsKey(uiEvtID))
        {
            EventDelegate evtDelegate = mDicEvtDelegate[uiEvtID];
            evtDelegate.SendEvent(objData);
        }
    }

    /// <summary>清空事件</summary>
    public void ClearEvent()
    {
        foreach (var item in mDicEvtDelegate)
        {
            item.Value.ClearCallBack();
        }
        mDicEvtDelegate.Clear();
    }
}