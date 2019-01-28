/*
 编辑器工具类
 创建时间: 2019.1.28
 脚本左右: 拓展编辑器
 */
using UnityEditor;
using UnityEngine;

/// <summary>编辑器工具类</summary>
public class UnilityEditor : Editor
{
    /*
     在右键菜单中创建一个新的菜单栏,创建此物体的父物体
     注:只拷贝大小和位置,其他的均需要初始化
         */
    /// <summary>创建父物体</summary>
    [MenuItem("GameObject/CreateParent", priority = 0)]
    static void CreateGameObject()
    {
        Transform child = Selection.activeGameObject.transform;

        GameObject parent = new GameObject(child.name + "_Parent");
        Transform pTrans = parent.transform;
        pTrans.position = child.position;
        pTrans.localScale = Vector3.one;
        pTrans.localEulerAngles = Vector3.zero;
        if (child.parent != null)
        {
            pTrans.parent = child.parent;
        }
        child.parent = pTrans;
    }
}
