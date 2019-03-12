using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UI管理类</summary>
public class UIManager : CSMonoSingleton<UIManager>
{
    #region List Operation
    /// <summary>打开展示的界面</summary>
    public Dictionary<Type, Dictionary<int, UIBase>> ShowPanelDictionary;
    /// <summary>隐藏的面板</summary>
    public Dictionary<Type, Dictionary<int, UIBase>> HidePanelDictionary;

    private void AddShowPanel(UIBase ui, int id)
    {
        if (ShowPanelDictionary != null)
        {
            Type type = ui.GetType();
            Dictionary<int, UIBase> uiPanels;
            if (ShowPanelDictionary.ContainsKey(type))
            {
                uiPanels = ShowPanelDictionary[type];
                if (uiPanels != null)
                {
                    if (uiPanels.ContainsKey(id))
                    {
                        Debug.Log("UI已存在,ID重复" + type + id);
                    }
                }
                else
                {
                    uiPanels = new Dictionary<int, UIBase>();
                }

            }
            else
            {
                uiPanels = new Dictionary<int, UIBase>();
                ShowPanelDictionary.Add(type, uiPanels);
            }
            uiPanels.Add(id, ui);
        }
    }
    private void RemoveShowPanel(UIBase ui, int id)
    {
        if (ShowPanelDictionary != null)
        {
            Type type = ui.GetType();
            Dictionary<int, UIBase> uiPanels;
            if (ShowPanelDictionary.TryGetValue(type, out uiPanels))
            {
                if (uiPanels != null)
                {
                    uiPanels.Remove(id);
                }
            }

        }
    }
    private void AddHidePanel(UIBase ui, int id)
    {
        if (HidePanelDictionary != null)
        {
            Type type = ui.GetType();
            Dictionary<int, UIBase> uiPanels;
            if (HidePanelDictionary.ContainsKey(type))
            {
                uiPanels = HidePanelDictionary[type];
                if (uiPanels != null)
                {
                    if (uiPanels.ContainsKey(id))
                    {
                        Debug.Log("UI已存在,ID重复" + type + id);
                    }
                }
                else
                {
                    uiPanels = new Dictionary<int, UIBase>();
                }

            }
            else
            {
                uiPanels = new Dictionary<int, UIBase>();
                HidePanelDictionary.Add(type, uiPanels);
            }
            uiPanels.Add(id, ui);
        }
    }
    private void RemoveHidePanel(UIBase ui, int id)
    {
        if (HidePanelDictionary != null)
        {
            Type type = ui.GetType();
            Dictionary<int, UIBase> uiPanels;
            if (HidePanelDictionary.TryGetValue(type, out uiPanels))
            {
                if (uiPanels != null)
                {
                    uiPanels.Remove(id);
                }
            }

        }
    }
    #endregion
    public override void Init()
    {
        base.Init();
        ShowPanelDictionary = new Dictionary<Type, Dictionary<int, UIBase>>();
        HidePanelDictionary = new Dictionary<Type, Dictionary<int, UIBase>>();
    }


    /// <summary>获取已打开的面板</summary>
    /// <typeparam name="T">面板类型</typeparam>
    /// <param name="uiid">id</param>
    /// <returns></returns>
    public T GetPanel<T>(int uiid = 0) where T : UIBase
    {
        Type type = typeof(T);
        if (ShowPanelDictionary != null)
        {
            Dictionary<int, UIBase> uiPanels;
            if (ShowPanelDictionary.TryGetValue(type, out uiPanels))
            {
                if (uiPanels != null && uiPanels.Count > 0)
                {
                    UIBase ui;
                    if (uiPanels.TryGetValue(uiid, out ui))
                    {
                        return ui as T;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Not Initialize UIManager");
        }
        return null;
    }
    /// <summary>获取隐藏的面板</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="uiid"></param>
    /// <returns></returns>
    public T GetHidePanel<T>(int uiid = 0) where T : UIBase
    {
        Type type = typeof(T);
        if (HidePanelDictionary != null)
        {
            Dictionary<int, UIBase> uiPanels;
            if (HidePanelDictionary.TryGetValue(type, out uiPanels))
            {
                if (uiPanels != null && uiPanels.Count > 0)
                {
                    UIBase ui;
                    if (uiPanels.TryGetValue(uiid, out ui))
                    {
                        return ui as T;
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Not Initialize UIManager");
        }
        return null;
    }
    /// <summary>界面是否打开</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <returns></returns>
    public bool IsOpenPanel<T>(int id = 0) where T : UIBase
    {
        T t = GetPanel<T>(id);
        if (t != null)
        {
            return true;
        }
        return false;
    }

    /// <summary>打开面板</summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="id"></param>
    /// <param name="isHasAudio"></param>
    /// <param name="cb"></param>
    public void OpenPanel<T>(int id = 0, DelegateOpenPanelCallBack cb = null) where T : UIBase
    {
        UIBase ui = GetPanel<T>(id);
        if (ui != null)
        {
            return;
        }
        ui = GetHidePanel<T>(id);
        if (ui != null)
        {
            if (cb!=null)
            {
                cb(ui);
            }

            RemoveHidePanel(ui, id);
            AddShowPanel(ui, id);
            return;
        }
        CSGame.Instance.StartCoroutine(LoadUIPanelPrefab<T>(id, cb));
    }

    private IEnumerator LoadUIPanelPrefab<T>(int id, DelegateOpenPanelCallBack cb) where T : UIBase
    {
        Type type = typeof(T);
        Entry entry = UIEnroll.Instance.FindEntry(type.Name);
        if (entry == null || entry.path == null || string.IsNullOrEmpty(entry.path))
        {
            UnityEngine.Debug.LogError("界面注册错误 : " + type.Name);
            yield break;
        }
        GameObject prefab = CSGame.Instance.GetStaticObj(entry.type.Name);
        if (prefab == null)
        {
            prefab = Resources.Load(entry.path + "/" + entry.typeName) as GameObject;
        }
        if (prefab == null)
        {
            UnityEngine.Debug.LogError("界面管理错误: 未找到预设体");
            yield break;
        }
        GameObject uiGame = Instantiate(prefab);
        uiGame.SetActive(true);
        UIBase ui = uiGame.GetComponent<T>();
        if (ui != null)
        {
            UILayerManager.Singleton.SetLayer(uiGame, ui.PanelLayerType);

            ui.Init();
            ui.Show();

            if (cb != null)
            {
                cb(ui);
            }

            AddShowPanel(ui, id);
        }
    }
    private void InstantiateUI(GameObject prefab)
    {

    }
}
/// <summary>打开面板回调</summary>
/// <param name="ui"></param>
public delegate void DelegateOpenPanelCallBack(UIBase ui);
