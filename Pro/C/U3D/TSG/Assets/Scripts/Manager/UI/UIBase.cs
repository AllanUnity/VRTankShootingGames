using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>UI基础类</summary>
public class UIBase : MonoBehaviour
{
    [SerializeField]
    private long uiPanelID = 0;
    /// <summary>界面唯一ID</summary>
    public long UIId { get { return uiPanelID; } }
    protected UILayerType mPanelLayerType = UILayerType.Window;
    /// <summary>窗口层级</summary>
    public virtual UILayerType PanelLayerType { get { return mPanelLayerType; } }
    /// <summary>客户端事件</summary>
    protected EventHanlderManager mClientEvent = new EventHanlderManager();

    public T Get<T>(string path) where T : UnityEngine.Object
    {
        return Utility.GetObject<T>(this.transform, path);
    }

    public T Get<T>(string name, ref T obj) where T : UnityEngine.Object
    {
        return obj ?? (obj = Get<T>(name));
    }
    /// <summary>初始化</summary>
    public virtual void Init()
    {
        mClientEvent.SendEvent(CEvent.OpenPanel, this);
    }
    /// <summary>展示</summary>
    public virtual void Show()
    {
        gameObject.SetActive(true);
        mClientEvent.SendEvent(CEvent.ShowPanel, this);
    }
    public virtual void OnUpdate()
    {

    }
    /// <summary>隐藏</summary>
    public virtual void Hide()
    {
        gameObject.SetActive(false);
        mClientEvent.SendEvent(CEvent.HidePanel, this);
    }
    /// <summary>关闭</summary>
    public virtual void Close()
    {
        mClientEvent.SendEvent(CEvent.ClosePanel, this);
        mClientEvent.ClearEvent();
        mClientEvent = null;
    }


    public bool IsActive()
    {
        return gameObject && gameObject.activeInHierarchy;
    }

    #region OnClick
    /// <summary>按钮添加点击事件</summary>
    /// <param name="_btn"></param>
    /// <param name="callback"></param>
    protected void AddButtonClickEvent(Button _btn,UnityEngine.Events.UnityAction callback)
    {
        if (_btn != null)
        {
            if (callback!=null)
            {
                _btn.onClick.AddListener(callback);
            }
            else
            {
                Debug.Log("Btn CallBack is None");
            }
        }
        else
        {
            Debug.Log("Add's Button is None");
        }
    }
    protected void AddToggleClickEvent(Toggle _tog, UnityEngine.Events.UnityAction<bool> callback)
    {
        if (_tog != null)
        {
            if (callback != null)
            {
                _tog.onValueChanged.AddListener(callback);
            }
            else
            {
                Debug.Log("Toggle CallBack is None");
            }
        }
        else
        {
            Debug.Log("Add's Toggle is None");
        }
    }
    #endregion
}
