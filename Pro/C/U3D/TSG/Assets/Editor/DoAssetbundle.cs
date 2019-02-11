using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DoAssetbundle
{

    [MenuItem("AssetBundles/Test")]
    static void Debugs()
    {
        //Debug.Log(_AssetBundlesPath);
        Object[] objs = Selection.objects;
        foreach (Object item in objs)
        {
            Debug.Log(item.GetType());
        }
    }
    #region 创建资源
    /// <summary>
    /// 资源路径, 将打包到Unity工程文件夹下的AB转为打到固定的文件夹下
    /// </summary>
    private static string _AssetBundlesPath
    {
        get
        {
            return "";
        }
    }
    /// <summary>
    /// 自动打包所有的资源(设置了AssetBundle Name的资源)
    /// </summary>
    [MenuItem("AssetBundles/Create All AssetBundles")]
    static void CreateAllAssetBundles()
    {
        Caching.ClearCache();

        //检测路径
        if (!Directory.Exists(_AssetBundlesPath))
        {
            Directory.CreateDirectory(_AssetBundlesPath);
        }

        BuildPipeline.BuildAssetBundles(_AssetBundlesPath, BuildAssetBundleOptions.ChunkBasedCompression, EditorUserBuildSettings.activeBuildTarget);
        UnityEngine.Debug.Log("Create All AssetBundles IS Over");
    }
    #endregion
    #region 设置
    public static List<string> _assetPathArray = new List<string>();
    /// <summary>
    /// 将某以文件夹中的资源进行分离打包, 把依赖资源分离打包
    /// </summary>
    [MenuItem("AssetBundles/Set AssetBundle Name")]
    static void SetMainAssetBundleName()
    {
        SetSonDirectoryInfo("Resources");

        for (int i = 0; i < _assetPathArray.Count; i++)
        {
            SetPrefabsName(_assetPathArray[i]);
        }
    }
    static void SetPrefabsName(string _path)
    {
        string fullPath = Application.dataPath + "/" + _path;
        if (Directory.Exists(fullPath))
        {
            EditorUtility.DisplayProgressBar("设置AssetBundle名称", "正在设置名称中...", 0f);
            DirectoryInfo dir = new DirectoryInfo(fullPath);//获取目录信息
            FileInfo[] files = dir.GetFiles("*", SearchOption.TopDirectoryOnly);//获取所有的文件信息

            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fileInfo = files[i];
                EditorUtility.DisplayProgressBar("Setting AB", string.Format("Setting {0} Name...", _path), 1f * i / files.Length);
                if (!fileInfo.Name.EndsWith(".meta"))   //判断去除掉扩展名为“.meta”的文件
                {
                    string basePath = "Assets" + fileInfo.FullName.Substring(Application.dataPath.Length);  //编辑器下路径Assets/..
                    string assetName = fileInfo.FullName.Substring(fullPath.Length);  //预设的Assetbundle名字，带上一级目录名称
                    assetName = assetName.Substring(0, assetName.LastIndexOf('.')); //名称要去除扩展名
                    assetName = assetName.Replace('\\', '/');   //注意此处的斜线一定要改成反斜线，否则不能设置名称
                    AssetImporter importer = AssetImporter.GetAtPath(basePath);
                    if (importer && importer.assetBundleName != assetName)
                    {
                        importer.assetBundleName = _path + assetName;  //设置预设的AssetBundleName名称
                    }
                }
            }
            EditorUtility.ClearProgressBar();   //清除进度条
        }
    }

    public static void SetSonDirectoryInfo(string _path)
    {
        string fullPath = Application.dataPath + "/" + _path + "/";
        if (Directory.Exists(fullPath))
        {
            DirectoryInfo dir = new DirectoryInfo(fullPath);//获取目录信息
            DirectoryInfo[] dirs = dir.GetDirectories("*");
            for (int i = 0; i < dirs.Length; i++)
            {
                SetSonDirectoryInfo(_path + "/" + dirs[i].Name);
                _assetPathArray.Add(_path + "/" + dirs[i].Name + "/");
            }
        }
    }
    #endregion
    #region 清除
    /// <summary>
    /// 清除之前设置过的ABName,避免产生不必要的资源
    /// </summary>
    [MenuItem("AssetBundles/Clear AssetBundle Name")]
    public static void ClearAssetBundlesName()
    {
        string[] oldAssetBundleNames = AssetDatabase.GetAllAssetBundleNames();
        EditorUtility.DisplayProgressBar("清除AssetBundleName", "正在清除AssetBundleName名称中...", 0f);//显示加载进度条

        for (int i = 0; i < oldAssetBundleNames.Length; i++)
        {
            EditorUtility.DisplayProgressBar("Clear AB'Name", string.Format("Clear {0}Names...", oldAssetBundleNames[i]), 1f * i / oldAssetBundleNames.Length);
            AssetDatabase.RemoveAssetBundleName(oldAssetBundleNames[i], true);
        }
        EditorUtility.ClearProgressBar();
    }
    #endregion
}
