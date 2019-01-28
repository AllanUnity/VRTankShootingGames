/*
 编辑器工具类
 创建时间: 2019.1.28
 脚本左右: 在Transform上增加3个按钮,用于分别恢复大小位置和角度
 */
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>拓展Transform, 添加按钮</summary>
[CustomEditor(typeof(Transform))]
public class OperationTransform : Editor
{
    public override void OnInspectorGUI()
    {
        OnResetTransformPRS();
    }
    /// <summary>初始化Transform的位置角度和缩放值的</summary>
    public void OnResetTransformPRS()
    {
        Transform trans = target as Transform;
        EditorGUIUtility.labelWidth = 15f;

        Vector3 pos;
        Vector3 rot;
        Vector3 scale;

        EditorGUILayout.BeginHorizontal();
        {
            if (DrawButton("P", "Reset Position", IsResetPositionValid(trans), 20f))
            {
                trans.localPosition = Vector3.zero;
            }
            pos = DrawVector3(trans.localPosition);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (DrawButton("R", "Reset Rotation", IsResetRotationValid(trans), 20f))
            {
                trans.localEulerAngles = Vector3.zero;
            }
            rot = DrawVector3(trans.localEulerAngles);
        }
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        {
            if (DrawButton("S", "Reset Scale", IsResetScaleValid(trans), 20f))
            {
                trans.localScale = Vector3.one;
            }
            scale = DrawVector3(trans.localScale);
        }
        EditorGUILayout.EndHorizontal();

        // If something changes, set the transform values
        if (GUI.changed)
        {
            trans.localPosition = Validate(pos);
            trans.localEulerAngles = Validate(rot);
            trans.localScale = Validate(scale);
        }
    }
    #region GUI按钮
    /// <summary>
    /// GUI按钮
    /// </summary>
    /// <param name="title">按钮显示名字</param>
    /// <param name="tooltip">鼠标悬停显示信息</param>
    /// <param name="enabled">是否可用</param>
    /// <param name="width">宽度</param>
    /// <returns></returns>

    static bool DrawButton(string title, string tooltip, bool enabled, float width)
    {
        if (enabled)
        {
            // Draw a regular button
            return GUILayout.Button(new GUIContent(title, tooltip), GUILayout.Width(width));
        }
        else//禁用
        {
            // Button should be disabled -- draw it darkened and ignore its return value
            Color color = GUI.color;
            GUI.color = new Color(1f, 1f, 1f, 0.25f);
            GUILayout.Button(new GUIContent(title, tooltip), GUILayout.Width(width));
            GUI.color = color;
            return false;
        }
    }
    #endregion
    /// <summary>
    /// Helper function that draws a field of 3 floats.
    /// </summary>

    static Vector3 DrawVector3(Vector3 value)
    {
        GUILayoutOption opt = GUILayout.MinWidth(30f);
        value.x = EditorGUILayout.FloatField("X", value.x, opt);
        value.y = EditorGUILayout.FloatField("Y", value.y, opt);
        value.z = EditorGUILayout.FloatField("Z", value.z, opt);
        return value;
    }
    #region 是否为初始值
    /// <summary>
    /// Helper function that determines whether its worth it to show the reset position button.
    /// </summary>

    static bool IsResetPositionValid(Transform targetTransform)
    {
        Vector3 v = targetTransform.localPosition;
        return (v.x != 0f || v.y != 0f || v.z != 0f);
    }

    /// <summary>
    /// Helper function that determines whether its worth it to show the reset rotation button.
    /// </summary>

    static bool IsResetRotationValid(Transform targetTransform)
    {
        Vector3 v = targetTransform.localEulerAngles;
        return (v.x != 0f || v.y != 0f || v.z != 0f);
    }

    /// <summary>
    /// Helper function that determines whether its worth it to show the reset scale button.
    /// </summary>

    static bool IsResetScaleValid(Transform targetTransform)
    {
        Vector3 v = targetTransform.localScale;
        return (v.x != 1f || v.y != 1f || v.z != 1f);
    }
    #endregion

    /// <summary>
    /// Helper function that removes not-a-number values from the vector.
    /// </summary>

    static Vector3 Validate(Vector3 vector)
    {
        vector.x = float.IsNaN(vector.x) ? 0f : vector.x;
        vector.y = float.IsNaN(vector.y) ? 0f : vector.y;
        vector.z = float.IsNaN(vector.z) ? 0f : vector.z;
        return vector;
    }
}
