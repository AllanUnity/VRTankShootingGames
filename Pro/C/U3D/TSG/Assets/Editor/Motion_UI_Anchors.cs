using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// 自动绑定UI的锚点
/// </summary>
public class Motion_UI_Anchors : Editor
{
    [MenuItem("UITool/自动对齐锚点")]
    public static void SetAnchorsEditor()
    {
        Transform[] transforms = Selection.transforms;//鼠标选中的物体
        foreach (Transform item in transforms)
        {
            SetAnchors(item.gameObject.GetComponent<RectTransform>());
        }
    }
    static void SetAnchors(RectTransform itemRect)
    {
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        if (itemRect.pivot == pivot)
        {

            #region 把锚点放在父物体的中心点上
            Vector3 localPos = itemRect.localPosition;
            Rect s_rect = itemRect.rect;
            itemRect.anchorMax = new Vector2(0.5f, 0.5f);
            itemRect.anchorMin = new Vector2(0.5f, 0.5f);
            itemRect.sizeDelta = new Vector2(s_rect.width, s_rect.height);//设置大小不变
            itemRect.localPosition = localPos;
            #endregion

            #region 获取父物体的信息
            RectTransform p_rectTransform = itemRect.parent.GetComponent<RectTransform>();
            Rect p_rect = p_rectTransform.rect;
            #endregion
            #region 设置anchorMin和anchorMax
            //左边距离父物体左边的距离
            float s_l_distance = p_rect.width / 2 + (itemRect.localPosition.x - itemRect.sizeDelta.x / 2);
            float s_r_distance = p_rect.width / 2 + (itemRect.localPosition.x + itemRect.sizeDelta.x / 2);
            float s_u_distance = p_rect.height / 2 + (itemRect.localPosition.y - itemRect.sizeDelta.y / 2);
            float s_d_distance = p_rect.height / 2 + (itemRect.localPosition.y + itemRect.sizeDelta.y / 2);

            float minX = s_l_distance / p_rect.width;
            float maxX = s_r_distance / p_rect.width;
            float minY = s_u_distance / p_rect.height;
            float maxY = s_d_distance / p_rect.height;

            itemRect.anchorMin = new Vector2(minX, minY);
            itemRect.anchorMax = new Vector2(maxX, maxY);
            itemRect.sizeDelta = Vector2.zero;
            itemRect.anchoredPosition = Vector3.zero;
            #endregion
        }
        foreach (Transform item in itemRect.transform)
        {
            SetAnchors(item.gameObject.GetComponent<RectTransform>());
        }
    }
    [MenuItem("UITool/对齐自身锚点")]
    public static void SetAnchorsSelfEditor()
    {
        Transform[] transforms = Selection.transforms;//鼠标选中的物体
        foreach (Transform item in transforms)
        {
            SetOneAnchors(item.gameObject.GetComponent<RectTransform>());
        }
    }
    static void SetOneAnchors(RectTransform itemRect)
    {
        Vector2 pivot = new Vector2(0.5f, 0.5f);
        if (itemRect.pivot == pivot)
        {

            #region 把锚点放在父物体的中心点上
            Vector3 localPos = itemRect.localPosition;
            Rect s_rect = itemRect.rect;
            itemRect.anchorMax = new Vector2(0.5f, 0.5f);
            itemRect.anchorMin = new Vector2(0.5f, 0.5f);
            itemRect.sizeDelta = new Vector2(s_rect.width, s_rect.height);//设置大小不变
            itemRect.localPosition = localPos;
            #endregion

            #region 获取父物体的信息
            RectTransform p_rectTransform = itemRect.parent.GetComponent<RectTransform>();
            Rect p_rect = p_rectTransform.rect;
            #endregion
            #region 设置anchorMin和anchorMax
            //左边距离父物体左边的距离
            float s_l_distance = p_rect.width / 2 + (itemRect.localPosition.x - itemRect.sizeDelta.x / 2);
            float s_r_distance = p_rect.width / 2 + (itemRect.localPosition.x + itemRect.sizeDelta.x / 2);
            float s_u_distance = p_rect.height / 2 + (itemRect.localPosition.y - itemRect.sizeDelta.y / 2);
            float s_d_distance = p_rect.height / 2 + (itemRect.localPosition.y + itemRect.sizeDelta.y / 2);

            float minX = s_l_distance / p_rect.width;
            float maxX = s_r_distance / p_rect.width;
            float minY = s_u_distance / p_rect.height;
            float maxY = s_d_distance / p_rect.height;

            itemRect.anchorMin = new Vector2(minX, minY);
            itemRect.anchorMax = new Vector2(maxX, maxY);
            itemRect.sizeDelta = Vector2.zero;
            itemRect.anchoredPosition = Vector3.zero;
            #endregion
        }
    }
    [MenuItem("UITool/排列子物体8")]
    public static void SetGrids()
    {
        Transform[] transforms = Selection.transforms;//鼠标选中的物体
        foreach (Transform item in transforms)
        {
            SetGrid(item);
        }
    }
    public static void SetGrid(Transform Trans)
    {
        /// <summary>自身方块</summary>
        RectTransform _rect;
        _rect = Trans.GetComponent<RectTransform>();
        int left = 5;
        int top = 8;
        List<Transform> _trans = new List<Transform>();
        for (int i = 0; i < Trans.childCount; i++)
        {
            _trans.Add(Trans.GetChild(i));
        }

        RectTransform _rect0 = _trans[0].gameObject.GetComponent<RectTransform>();
        Vector3 markPoint = new Vector3(_rect0.sizeDelta.x / 2 - _rect.sizeDelta.x / 2 + left, -_rect0.sizeDelta.y / 2 + _rect.sizeDelta.y / 2 -10, 0);
        _trans[0].localPosition = markPoint;

        for (int i = 1; i < _trans.Count; i++)
        {
            RectTransform _recti = _trans[i].gameObject.GetComponent<RectTransform>();
            RectTransform _rectf = _trans[i - 1].gameObject.GetComponent<RectTransform>();
            Vector3 point = new Vector3(_recti.sizeDelta.x / 2 - _rect.sizeDelta.x / 2 + left, _rectf.localPosition.y - _rectf.sizeDelta.y / 2 - top - _recti.sizeDelta.y / 2, 0);
            UnityEngine.Debug.Log("排序" + i + point);
            _trans[i].localPosition = point;
        }
    }
    [MenuItem("UITool/排列子物体0")]
    public static void SetGrids0()
    {
        Transform[] transforms = Selection.transforms;//鼠标选中的物体
        foreach (Transform item in transforms)
        {
            SetGrid0(item);
        }
    }
    public static void SetGrid0(Transform Trans)
    {
        /// <summary>自身方块</summary>
        RectTransform _rect;
        _rect = Trans.GetComponent<RectTransform>();
        int left = 0;
        int top = 0;
        List<Transform> _trans = new List<Transform>();
        for (int i = 0; i < Trans.childCount; i++)
        {
            _trans.Add(Trans.GetChild(i));
        }

        RectTransform _rect0 = _trans[0].gameObject.GetComponent<RectTransform>();
        Vector3 markPoint = new Vector3(_rect0.sizeDelta.x / 2 - _rect.sizeDelta.x / 2 + left+2, -_rect0.sizeDelta.y / 2 + _rect.sizeDelta.y / 2-2, 0);
        _trans[0].localPosition = markPoint;

        for (int i = 1; i < _trans.Count; i++)
        {
            RectTransform _recti = _trans[i].gameObject.GetComponent<RectTransform>();
            RectTransform _rectf = _trans[i - 1].gameObject.GetComponent<RectTransform>();
            Vector3 point = new Vector3(_recti.sizeDelta.x / 2 - _rect.sizeDelta.x / 2 + left+2, _rectf.localPosition.y - _rectf.sizeDelta.y / 2 - top - _recti.sizeDelta.y / 2, 0);
            UnityEngine.Debug.Log("排序" + i + point);
            _trans[i].localPosition = point;
        }
    }
}
