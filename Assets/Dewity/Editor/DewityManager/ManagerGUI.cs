using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;


///<summary>
///脚本名称： ChatWindow.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class ManagerGUI : EditorWindow
{
    private string currentMsg;

    [MenuItem("Dewity/控制面板")]
    public static void OpenWindow()
    {
        ManagerGUI window = GetWindow<ManagerGUI>("Dewity 控制面板");
        window.OnEnable();
    }

    public void OnEnable()
    {
        // 强制重绘EditorWindow
        Repaint();
    }

    public void OnGUI()
    {

        if(GUILayout.Button("添加预制体"))
        {
            
        }

        
    }

}