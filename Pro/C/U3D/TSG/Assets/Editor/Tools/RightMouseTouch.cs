using System.Collections.Generic;
using UnilityTool;
using UnityEditor;
using UnityEngine;

public class RightMouseTouch
{

    static List<string> pathArray = new List<string>();
    [MenuItem("Assets/Tools/SVN日志")]
    static void SVNLogRight()
    {
        Debug.LogError("SVN日志");
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.LogSVN(pathArray);
    }
    [MenuItem("Assets/Tools/SVN更新")]
    static void SVNUpdateRight()
    {
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.UpdateSVN(pathArray, Application.dataPath.Replace("Client/Branch/Client/Assets", "Data/Branch/CurrentUseData/wzcq_20170110/"));//??????????
    }
    [MenuItem("Assets/Tools/SVN提交")]
    public static void SVNCommitRight()
    {
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.CommitToSVN(pathArray);
    }
    [MenuItem("Assets/Tools/SVN还原")]
    public static void SVNRevertRight()
    {
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.RevertSVN(pathArray);
    }
    [MenuItem("Assets/Tools/SVN解决冲突")]
    public static void SVNResolveRight()
    {
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.ResolveSVN(pathArray);
    }
    [MenuItem("Assets/Tools/SVN追溯")]
    public static void SVNBlameRight()
    {
        pathArray.Clear();
        FileUnility.GetSelectAssetPaths(ref pathArray);
        FileUnility.BlameSVN(pathArray);
    }

}
