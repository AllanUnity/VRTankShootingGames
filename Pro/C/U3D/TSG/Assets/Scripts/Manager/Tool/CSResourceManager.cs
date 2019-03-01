using UnityEngine;

/// <summary>资源加载管理</summary>
public class CSResourceManager : CSMonoSingleton<CSResourceManager>
{
    public GameObject LoadUIPanel(string path)
    {
        GameObject go = null;
        UnityEngine.Debug.Log("加载UI资源:" + path);

        if (go == null)
        {
            go = Resources.Load("UI/view/" + path) as GameObject;
        }
        return go;
    }
}
