using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


///<summary>
///脚本名称： ChatWindow.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

public class ChatWindow : EditorWindow
{
    private Vector2 scrollPos = Vector2.zero;
    private List<string> msgs = new List<string>();
    private string currentMsg;
    private ChatApi api = new ChatApi();

    [MenuItem("ChatGPT/打开窗口")]
    public static void OpenWindow()
    {
        Debug.LogWarning("ChatGPT接口由第三方提供。");
        ChatWindow window = GetWindow<ChatWindow>("ChatGPT");
        window.OnEnable();
    }

    public void OnEnable()
    {
        // 强制重绘EditorWindow
        Repaint();
    }

    public void OnGUI()
    {
        // 绘制滚动视图
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.HelpBox("你好！我是ChatGPT，可以为你解答使用Unity中遇到的困惑。", MessageType.Info);
        EditorGUILayout.Space();
        EditorGUILayout.BeginVertical();
        foreach (var msg in msgs)
        {
            // 将 LabelField 改为 TextArea，并限制高度
            var style = new GUIStyle(EditorStyles.textArea);
            style.fixedHeight = 0; // 自动调整高度
            EditorGUILayout.TextArea(msg, style, GUILayout.ExpandHeight(true));
        }

        EditorGUILayout.EndVertical();
        EditorGUILayout.EndScrollView();

        // 绘制文本输入框和发送按钮
        EditorGUILayout.BeginHorizontal();
        currentMsg = EditorGUILayout.TextField(currentMsg);
        if (GUILayout.Button("发送", GUILayout.ExpandWidth(false), GUILayout.Width(60)))
        {
            if (!string.IsNullOrEmpty(currentMsg))
            {
                msgs.Add("ME:   " + currentMsg);
                msgs.Add(api.Get(currentMsg));
                currentMsg = string.Empty;
                Repaint(); // 强制重绘EditorWindow
            }

            if (msgs.Count > 0)
            {
                // 确保焦点在发送按钮上
                EditorGUI.FocusTextInControl("发送");
            }
        }
        
        EditorGUILayout.EndHorizontal();
        
    }

}