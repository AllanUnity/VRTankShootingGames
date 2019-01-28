using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

namespace UnilityTool
{
    public static class FileUnility
    {
        public static void GetSelectAssetPaths(ref List<string> strName)
        {

            UnityEngine.Object[] mFile = FileUnility.GetAssetsFiltered();
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
}
