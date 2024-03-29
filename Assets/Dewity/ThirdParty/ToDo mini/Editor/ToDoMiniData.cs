﻿using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ToDoMini
{
    /// <summary>
    /// 汉化by 蛋壳 （Github@DanKE123abc）
    /// </summary>
    public class ToDoMiniData : ScriptableObject
    {
        [Header("显示密度")]
        public ToDoMini.DisplayDensity displayDensity;
        [Header("保留已完成的任务")]
        public bool keepCompletedTasks = true;
        [HideInInspector]
        public bool showCompletedTasks;
        [Space]

        [HideInInspector]
        public List<TodoItem> items = new List<TodoItem>();

        public void AddTask(string task)
        {
            TodoItem item = new TodoItem(task);
            items.Add(item);
        }

        public void DeleteCompletedTasks()
        {
            List<int> indexesToRemove = new List<int>();
            for (int i = 0; i < items.Count; i++)
                if (items[i].isComplete)
                    indexesToRemove.Add(i);
            for (int i = indexesToRemove.Count - 1; i >= 0; i--)
                items.RemoveAt(indexesToRemove[i]);
        }
    }

    /// <summary>
    /// 汉化by 蛋壳 （Github@DanKE123abc）
    /// </summary>
    [Serializable]
    public class TodoItem
    {
        public string task;
        public bool isComplete;

        public TodoItem(string _task)
        {
            task = _task;
        }
    }

    /// <summary>
    /// 汉化by 蛋壳 （Github@DanKE123abc）
    /// </summary>
    [Serializable]
    public class TodoMiniDataExport
    {
        public List<TodoItem> tasks;

        public TodoMiniDataExport(ToDoMiniData data)
        {
            tasks = data.items;
        }
    }


    /// <summary>
    /// 汉化by 蛋壳 （Github@DanKE123abc）
    /// </summary>
    [CustomEditor(typeof(ToDoMiniData))]
    class ToDoMiniDataEditor : Editor
    {
        string SavingDirectory {
            get {
                if (Directory.Exists(Application.dataPath + assetsSubdirectory))
                    return Application.dataPath + assetsSubdirectory;
                else
                    return Application.dataPath + "/";
            }
        }
        readonly string assetsSubdirectory = "Assets/";
        readonly string exportFileName = "导出任务清单.json";


        public override void OnInspectorGUI()
        {
            ToDoMiniData data = (ToDoMiniData)target;

            GUILayout.Space(5);
            GUILayout.Label("设置", EditorStyles.boldLabel);

            GUILayout.Space(2);
            EditorGUI.BeginChangeCheck();
            EditorGUILayout.PropertyField(serializedObject.FindProperty("displayDensity"));
            EditorGUILayout.PropertyField(serializedObject.FindProperty("keepCompletedTasks"));
            serializedObject.ApplyModifiedProperties();
            if (EditorGUI.EndChangeCheck())
            {
                if (!data.keepCompletedTasks)
                    DeleteCompletedTasks(ref data);
                UpdateToDoWindow();
            }

            // Clear completed tasks.
            if (GUILayout.Button("☑ 删除已完成的任务"))
            {
                DeleteCompletedTasks(ref data);
                UpdateToDoWindow();
            }

            // Clear all tasks.
            GUILayout.Space(2);
            if (GUILayout.Button("✖ 删除所有任务"))
            {
                Undo.RecordObject(data, "删除所有任务");
                data.items.Clear();
                EditorUtility.SetDirty(data);
                UpdateToDoWindow();
            }

            // Import/export.
            GUILayout.Space(10);
            GUILayout.Label("导入/导出", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();

            // Import tasks.
            DrawDropArea("导入 (将文件拖到此处)", (TextAsset droppedTextAsset) => ImportData(droppedTextAsset, ref data));
            GUILayout.Space(5);

            // Export tasks.
            GUIStyle buttonStyle = new GUIStyle(GUI.skin.button);
            buttonStyle.margin = new RectOffset();
            if (GUILayout.Button("导出", buttonStyle, GUILayout.Height(30)))
                ExportData(ref data);
            GUILayout.EndHorizontal();
        }


        void DeleteCompletedTasks(ref ToDoMiniData data)
        {
            Undo.RecordObject(data, "删除已完成的任务");
            data.DeleteCompletedTasks();
            EditorUtility.SetDirty(data);
            AssetDatabase.SaveAssets();
        }

        void ImportData(TextAsset import, ref ToDoMiniData existingData)
        {
            TodoMiniDataExport parsedData = (TodoMiniDataExport)JsonUtility.FromJson(import.text, typeof(TodoMiniDataExport));
            Undo.RecordObject(existingData, "ToDo 清单 导入");
            existingData.items.AddRange(parsedData.tasks);
            UpdateToDoWindow();
        }

        void ExportData(ref ToDoMiniData data)
        {
            string dataInJson = JsonUtility.ToJson(new TodoMiniDataExport(data));
            string fileName = GetSafeFileName(SavingDirectory, exportFileName);
            File.WriteAllText(SavingDirectory + fileName, dataInJson);
            AssetDatabase.Refresh();
            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath(assetsSubdirectory + fileName, typeof(UnityEngine.Object)));
        }

        void DrawDropArea<T>(string boxText, Action<T> ObjectAction) where T : UnityEngine.Object
        {
            Event currentEvent = Event.current;
            Rect boxRect = GUILayoutUtility.GetRect(50, 30, GUILayout.ExpandWidth(true));
            GUIStyle boxStyle = new GUIStyle(GUI.skin.box);
            boxStyle.alignment = TextAnchor.MiddleCenter;
            GUI.Box(boxRect, boxText, boxStyle);

            switch (currentEvent.type)
            {
                case EventType.DragUpdated:
                case EventType.DragPerform:
                    if (!boxRect.Contains(currentEvent.mousePosition))
                        break;

                    DragAndDrop.visualMode = DragAndDropVisualMode.Copy;
                    if (currentEvent.type == EventType.DragPerform)
                    {
                        DragAndDrop.AcceptDrag();

                        foreach (UnityEngine.Object droppedObject in DragAndDrop.objectReferences)
                            if (droppedObject is T)
                                ObjectAction((T)droppedObject);
                    }
                    break;
            }
        }

        void UpdateToDoWindow()
        {
            ToDoMini.shouldRefreshData = true;
            EditorWindow.GetWindow(typeof(ToDoMini));
        }

        static string GetSafeFileName(string directory, string desiredFileName)
        {
            FileInfo file = new FileInfo(directory + desiredFileName);
            int fileIndex = 0;
            while (file.Exists)
            {
                fileIndex++;
                file = new FileInfo(directory + desiredFileName.Insert(desiredFileName.Length - file.Extension.Length, " (" + fileIndex + ")"));
            }
            return file.Name;
        }
    }
}