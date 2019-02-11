using AssetBundles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>资源加载管理</summary>
public class CSResourceManager : CSMonoSingleton<CSResourceManager>
{
  public GameObject LoadUIPanel(string path)
    {
        GameObject go = null;
        UnityEngine.Debug.Log("加载UI资源:" + path);

        if (go==null)
        {
            go = Resources.Load("UI/view/" + path) as GameObject;
            //go=
        }

        return go;
    }
    //public static T LoadAsset<T>(string path, string assetName) where T : UnityEngine.Object
    //{
//#if UNITY_EDITOR
//        //if (CSGame.Instance.IsLoadLocalRex)
//        //{

//            //UnityEngine.Debug.Log(path.ToLower() + assetName.ToLower() + "---" + assetName);
//            string[] assetPaths = UnityEditor.AssetDatabase.GetAssetPathsFromAssetBundleAndAssetName(path.ToLower() + assetName.ToLower(), assetName);
//            if (assetName.Length == 0)
//            {
//                if (Debug.developerConsoleVisible) Debug.LogError("There Is No Asset With Name \"" + assetName + "\"in " + assetName);
//                return null;
//            }
//            UnityEngine.Object target = UnityEditor.AssetDatabase.LoadMainAssetAtPath(assetPaths[0]);
//            return target as T;
//        //}
//#endif

        //LoadedAssetBundle assetBundle = AssetBundleManager.LoadUIAssetAsync(path.ToLower() + assetName.ToLower());
        //if (assetBundle == null)
        //    return null;

        //return assetBundle.m_AssetBundle.LoadAsset<T>(assetName);
    //}
}
