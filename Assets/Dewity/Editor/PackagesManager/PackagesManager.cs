using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEditor;
using LitJson;

namespace Dewity
{
    
    public class PackagesManager : EditorWindow
    {
        private static string jsonString;
        private Vector2 scrollPosition;
        private JsonData jsonData;
        private GUIStyle myLabelStyle;

        [MenuItem("Dewity/推荐插件")]
        public static async Task ShowWindow()
        {
            GetWindow<PackagesManager>("推荐插件");
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    jsonString = await client.GetStringAsync("https://dkdk.eu.org/dewity/packages.json");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"请求出错: {ex.Message}");
                }
            }
        }

        private void OnGUI()
        {
            myLabelStyle = new GUIStyle(GUI.skin.label);
            myLabelStyle.fontSize = 12;
            myLabelStyle.alignment = TextAnchor.MiddleLeft;
            if (jsonData == null)
            {
                jsonData = JsonMapper.ToObject(jsonString);
            }

            scrollPosition = GUILayout.BeginScrollView(scrollPosition);
            
            GUILayout.Label("推荐插件列表", EditorStyles.boldLabel);
            Assembly assembly = Assembly.GetAssembly(typeof(SceneView));
            Type logEntries = assembly.GetType("UnityEditor.LogEntries");
            MethodInfo clearConsoleMethod = logEntries.GetMethod("Clear");
            clearConsoleMethod.Invoke(new object(), null);
            JsonData packages = jsonData["packages"];
            if (packages != null && packages.IsArray)
            {
                for (int i = 0; i < packages.Count; i++)
                {
                    JsonData package = packages[i];
                    string name = package["name"].ToString();
                    string description = package["description"].ToString();
                    string downloadUrl = package["download_url"].ToString();

                    GUILayout.Space(10f);
                    GUILayout.Label("插件名字: " + name,myLabelStyle);
                    GUILayout.Label("插件描述: " + description,myLabelStyle);

                    if (GUILayout.Button("安装"))
                    {
                        if (File.Exists(Application.dataPath+"/Dewity/Editor/PackagesManager/Download/packages.unitypackage"))
                        {
                            File.Delete(Application.dataPath+"/Dewity/Editor/PackagesManager/Download/packages.unitypackage");
                        }
                        //Application.OpenURL(downloadUrl);
                        Requests.Download(downloadUrl,Application.dataPath+"/Dewity/Editor/PackagesManager/Download/packages.unitypackage");
                        Debug.Log(name+"下载完成");
                        AssetDatabase.ImportPackage(Application.dataPath+"/Dewity/Editor/PackagesManager/Download/packages.unitypackage", true);
                    }
                }
            }

            GUILayout.EndScrollView();
        }
    }

}