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
    private Dictionary<UILayerType, Transform> LayerGameObjects = new Dictionary<UILayerType, Transform>();
    private Transform canvas;
    void InitLayer()
    {
        if (canvas==null)
        {
            canvas = CSGame.Instance.MainCanvas;
        }
        string[] LayerName = Enum.GetNames(typeof(UILayerType));
        for (int i = 0; i < LayerName.Length; i++)
        {
            UILayerType type = (UILayerType)System.Enum.Parse(typeof(UILayerType), LayerName[i]);

            GameObject _ui = new GameObject(LayerName[i]);
            LayerGameObjects.Add(type, _ui.transform);

            Transform trans = _ui.transform;
            trans.SetParent(canvas);

            RectTransform _rect = _ui.AddComponent<RectTransform>();
            _rect.localScale = Vector3.one;
            _rect.localPosition = Vector3.zero;
            _rect.localEulerAngles = Vector3.zero;
            _rect.sizeDelta = CSGame.Instance.SceneSize;
        }
    }


}
