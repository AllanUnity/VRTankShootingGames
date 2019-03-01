using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>UI层级管理器</summary>
public class UILayerManager : CSMonoSingleton<UILayerManager>
{
    public override void Init()
    {
        base.Init();
        InitLayer();
    }
    private Dictionary<UILayerType, GameObject> LayerGameObjects;
    private Transform canvas;
    void InitLayer()
    {
        if (canvas == null)
        {
            canvas = CSGame.Instance.MainCanvas;
        }
        LayerGameObjects = new Dictionary<UILayerType, GameObject>();
        string[] LayerName = Enum.GetNames(typeof(UILayerType));
        for (int i = 0; i < LayerName.Length; i++)
        {
            UILayerType type = (UILayerType)System.Enum.Parse(typeof(UILayerType), LayerName[i]);

            GameObject _ui = new GameObject(LayerName[i]);
            LayerGameObjects.Add(type, _ui);

            Transform trans = _ui.transform;
            trans.SetParent(canvas);

            RectTransform _rect = _ui.AddComponent<RectTransform>();
            _rect.localScale = Vector3.one;
            _rect.localPosition = Vector3.zero;
            _rect.localEulerAngles = Vector3.zero;
            _rect.sizeDelta = CSGame.Instance.SceneSize;
        }
    }

    public void SetLayer(GameObject current, UILayerType type)
    {
        if (LayerGameObjects == null || !LayerGameObjects.ContainsKey(type) || LayerGameObjects[type] == null)
        {
            InitLayer();
        }
        UnityEngine.Debug.Log("<color=red>设置层级" + current.name + "</color> => " + LayerGameObjects[type].transform.name);
        current.transform.SetParent(LayerGameObjects[type].transform);
        current.transform.localPosition = Vector3.zero;
        current.transform.localScale = Vector3.one;
        current.transform.localEulerAngles = Vector3.zero;
        current.transform.SetAsLastSibling();
    }


}
