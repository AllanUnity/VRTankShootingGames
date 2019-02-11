using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

/// <summary>
/// 生成代码
/// </summary>
public class CreateUIPanelScript
{
    public static Transform UITransPanel;
    [MenuItem("CodeScript/Code UI Panel Script")]
    public static void CreateUGUIPanel()
    {
        uiDic = new Dictionary<Type, List<string>>();
        if (!Directory.Exists(Application.dataPath + "/TempUIScripts"))
        {
            Directory.CreateDirectory(Application.dataPath + "/TempUIScripts");
        }
        GameObject _selectionUI = Selection.activeGameObject;

        if (_selectionUI == null)
        {
            UnityEngine.Debug.LogError("未选中任何UI");
            return;
        }
        UITransPanel = _selectionUI.transform;
        //新建文件
        string tmpPath = Application.dataPath + "/TempUIScripts/" + _selectionUI.name + ".cs";
        StreamWriter tmWriter = File.CreateText(tmpPath);

        #region using
        tmWriter.WriteLine("using UnityEngine;");
        tmWriter.WriteLine("using System.Collections.Generic;");
        tmWriter.WriteLine("using UnityEngine.UI;");
        #endregion

        #region class
        tmWriter.WriteLine("public class {0} : UIBase", _selectionUI.name);
        tmWriter.WriteLine("{");

        SearchSon(_selectionUI.transform, tmWriter);

        tmWriter.WriteLine("    public override void Init()");
        tmWriter.WriteLine("    {");
        tmWriter.WriteLine("        base.Init();");
        WriteAddEvent(tmWriter);
        tmWriter.WriteLine("    }");

        tmWriter.WriteLine("    public override void Show(bool isHasAudio = true)");
        tmWriter.WriteLine("    {");
        tmWriter.WriteLine("        base.Show(isHasAudio);");
        tmWriter.WriteLine("    }");

        tmWriter.WriteLine("    public override void Hide(bool destory, bool isHasAudio = true)");
        tmWriter.WriteLine("    {");
        tmWriter.WriteLine("        base.Hide(destory, isHasAudio);");
        tmWriter.WriteLine("    }");

        tmWriter.WriteLine("    public override void Destroy()");
        tmWriter.WriteLine("    {");
        tmWriter.WriteLine("        base.Destroy();");
        tmWriter.WriteLine("    }");

        WriteEventFunction(tmWriter);
        tmWriter.WriteLine("}");
        #endregion

        tmWriter.Flush();
        tmWriter.Close();
        UnityEngine.Debug.Log("生成UI代码完毕");
        AssetDatabase.Refresh();//刷新
    }
    public static void SearchSon(Transform trans, StreamWriter sw)
    {
        foreach (Transform item in trans)
        {
            WriteFunction<Button>(item, sw);
            WriteFunction<Toggle>(item, sw);
            WriteFunction<InputField>(item, sw);
            WriteFunction<Dropdown>(item, sw);
            SearchSon(item, sw);
        }
    }
    public static Dictionary<Type, List<string>> uiDic;

    public static void WriteFunction<T>(Transform trans, StreamWriter sw) where T : UIBehaviour
    {
        Type type = typeof(T);
        string UIName = trans.name;
        T t = trans.gameObject.GetComponent<T>();
        if (t != null)
        {
            if (uiDic.ContainsKey(type))
            {
                uiDic[type].Add(UIName);
            }
            else
            {
                uiDic.Add(type, new List<string>());
                uiDic[type].Add(UIName);
            }
            string transPath = ReadTransPath(UITransPanel, trans);
            sw.WriteLine("    private " + type.Name + " _" + UIName + " = null;");
            sw.WriteLine("    private " + type.Name + " " + UIName + " { get { return _" + UIName + " ?? (_" + UIName + " = Get<" + type.Name + ">(\"" + transPath + "\", transform)); } }");
        }
    }

    public static string ReadTransPath(Transform parent, Transform son)
    {
        if (son.parent == parent)
        {
            return parent.name + "/" + son.name;
        }
        else
        {
            return ReadTransPath(parent, son.parent) + "/" + son.name;
        }

    }
    public static void WriteAddEvent(StreamWriter sw)
    {
        foreach (KeyValuePair<Type, List<string>> item in uiDic)
        {
            foreach (string ui in item.Value)
            {
                switch (item.Key.Name)
                {
                    case "Button":
                        sw.WriteLine("        AddButtonOnClick(" + ui + ", " + ui + "OnClick);");
                        break;
                    case "Toggle":
                        sw.WriteLine("        AddToggleOnChanged(" + ui + ", " + ui + "OnValueChanged);");
                        break;
                    case "InputField":
                        sw.WriteLine("        AddInputFieldOnValueChanged(" + ui + ", " + ui + "OnValueChanged);");
                        sw.WriteLine("        AddInputFieldOnEndEdit(" + ui + ", " + ui + "OnEndEdit);");
                        break;
                    case "Dropdown":
                        sw.WriteLine("        AddDropdownOnValueChanged(" + ui + "," + ui + "OnValueChanged);");
                        break;
                    default:
                        UnityEngine.Debug.Log("需要增加对应的响应方法" + item.Key.Name);
                        break;
                }
            }
        }
    }
    public static void WriteEventFunction(StreamWriter sw)
    {
        foreach (KeyValuePair<Type, List<string>> item in uiDic)
        {
            foreach (string ui in item.Value)
            {
                switch (item.Key.Name)
                {
                    case "Button":
                        sw.WriteLine("    private void " + ui + "OnClick() { }");
                        break;
                    case "Toggle":
                        sw.WriteLine("    private void " + ui + "OnValueChanged(bool arg0) { }");
                        break;
                    case "InputField":
                        sw.WriteLine("    private void " + ui + "OnValueChanged(string arg0) { }");
                        sw.WriteLine("    private void " + ui + "OnEndEdit(string arg0) { }");
                        break;
                    case "Dropdown":
                        sw.WriteLine("    private void " + ui + "OnValueChanged(int arg0) { }");
                        break;
                    default:
                        UnityEngine.Debug.Log("需要增加对应的响应方法" + item.Key.Name);
                        break;
                }
            }

        }
    }
}
