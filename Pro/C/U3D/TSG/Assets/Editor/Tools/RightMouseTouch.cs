using System.Collections.Generic;
using System.Diagnostics;
using UnilityTool;
using UnityEditor;
using UnityEngine;

public class RightMouseTouch
{

    static List<string> pathArray = new List<string>();
    [MenuItem("Assets/Tools/SVN日志")]
    static void SVNLogRight()
    {
        UnityEngine.Debug.LogError("SVN日志");
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        LogSVN(pathArray);
    }
    //[MenuItem("Assets/Tools/SVN日志")]
    //static void SVNLogRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.LogSVN(pathArray);
    //}
    [MenuItem("Assets/Tools/SVN更新")]
    static void SVNUpdateRight()
    {
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        UpdateSVN(pathArray, Application.dataPath.Replace("Client/Branch/Client/Assets", "Data/Branch/CurrentUseData/wzcq_20170110/"));//??????????
    }
    //[MenuItem("Assets/Tools/SVN更新")]
    //static void SVNUpdateRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.UpdateSVN(pathArray, Application.dataPath.Replace("Client/Branch/Client/Assets", "Data/Branch/CurrentUseData/wzcq_20170110/"));//??????????
    //}
    [MenuItem("Assets/Tools/SVN提交")]
    public static void SVNCommitRight()
    {
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        CommitToSVN(pathArray);
    }
    //[MenuItem("Assets/Tools/SVN提交")]
    //public static void SVNCommitRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.CommitToSVN(pathArray);
    //}
    [MenuItem("Assets/Tools/SVN还原")]
    public static void SVNRevertRight()
    {
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        RevertSVN(pathArray);
    }
    //[MenuItem("Assets/Tools/SVN还原")]
    //public static void SVNRevertRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.RevertSVN(pathArray);
    //}
    [MenuItem("Assets/Tools/SVN解决冲突")]
    public static void SVNResolveRight()
    {
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        ResolveSVN(pathArray);
    }
    //[MenuItem("Assets/Tools/SVN解决冲突")]
    //public static void SVNResolveRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.ResolveSVN(pathArray);
    //}
    [MenuItem("Assets/Tools/SVN追溯")]
    public static void SVNBlameRight()
    {
        pathArray.Clear();
        GetSelectAssetPaths(ref pathArray);
        BlameSVN(pathArray);
    }
    //[MenuItem("Assets/Tools/SVN追溯")]
    //public static void SVNBlameRight()
    //{
    //    pathArray.Clear();
    //    FileUnility.GetSelectAssetPaths(ref pathArray);
    //    FileUnility.BlameSVN(pathArray);
    //}
    public static void GetSelectAssetPaths(ref List<string> strName)
    {

        UnityEngine.Object[] mFile = GetAssetsFiltered();
        for (int i = 0; i < mFile.Length; i++)
        {
            string path = AssetDatabase.GetAssetPath(mFile[i]);
            UnityEngine.Debug.Log(path);
            strName.Add(path);
        }
    }

    internal static void UpdateSVN(List<string> pathArray, string replace)
    {
        if (string.IsNullOrEmpty(replace))
        {
            ProcSVNCmd(GetPath(pathArray), "update");
        }
        else
        {
            ProcSVNCmd(GetPath(pathArray) + "*" + replace, "update");
        }
    }
    /// <summary>
    /// 提交
    /// </summary>
    /// <param name="pathArray"></param>
    internal static void CommitToSVN(List<string> pathArray)
    {
        ProcSVNCmd(GetPath(pathArray), "commit");
    }

    internal static void RevertSVN(List<string> pathArray)
    {
        ProcSVNCmd(GetPath(pathArray), "revert");
    }

    public static void LogSVN(List<string> pathArray)
    {
        ProcSVNCmd(GetPath(pathArray), "commit");
    }

    internal static void ResolveSVN(List<string> pathArray)
    {
        ProcSVNCmd(GetPath(pathArray), "resolve");
    }

    internal static void BlameSVN(List<string> pathArray)
    {
        ProcSVNCmd(GetPath(pathArray), "blame");
    }

    /// <summary>
    /// 获取资源
    /// </summary>
    /// <returns></returns>
    private static UnityEngine.Object[] GetAssetsFiltered()
    {
        return Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets);
    }
    /// <summary>
    /// 执行命令
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cmd"></param>
    static void ProcSVNCmd(string path, string cmd)
    {
        if (!string.IsNullOrEmpty(path))
        {
            Process process = Process.Start("TortoiseProc.exe", @"/command:" + cmd + " /path:" + path + " /closeonend:0");
            process.WaitForExit();
        }
        else
        {
            UnityEngine.Debug.Log("路径不对");
        }
    }
    private static string GetPath(List<string> dirs)
    {
        string path = "";
        string temp = Application.dataPath.Replace("Assets", "");
        for (int i = 0; i < dirs.Count; i++)
        {
            if (i == 0)
            {
                path += temp + dirs[i];
            }
            else
            {
                path += "*" + temp + dirs[i];
            }
        }
        return path;
    }
}
