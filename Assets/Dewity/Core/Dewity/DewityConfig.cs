using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dewity;
using LitJson;
using UnityEngine.Networking;

namespace Dewity
{

    public class DewityDataModel
    {
        public DewityData Dewity { get; set; }
        public ManagerConfigData ManagerConfig { get; set; }
    }

    public class DewityData
    {
        public string Framework { get; set; }
        public string Author { get; set; }
        public string Version { get; set; }
        public RepositoryData Repository { get; set; }
    }

    public class RepositoryData
    {
        public string Type { get; set; }
        public string Url { get; set; }
    }

    public class ManagerConfigData
    {
        public bool GameConsole { get; set; }
    }

    public class DewityConfig : MonoSingleton<DewityConfig>
    {

        private string filePath;
        private JsonData data;
        private DewityDataModel model;

        void Awake()
        {
            filePath = Application.streamingAssetsPath + "/DewityConfig.json";
            LoadJSON();
            ParseJSON();
        }

        public DewityDataModel GetModel()
        {
            return model;
        }

        private void LoadJSON()
        {
            string jsonContent = "";

            //if (Application.platform == RuntimePlatform.Android)
            //{
            //    WWW reader = new WWW(filePath);
            //    while (!reader.isDone)
            //    {
            //    }

            //    jsonContent = reader.text;
            //}
            //else
            //{
            //    jsonContent = File.ReadAllText(filePath);
            //}

            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    var bytes = new byte[fs.Length];
                    fs.Read(bytes, 0, (int)fs.Length);
                    string jsonData = Encoding.UTF8.GetString(bytes);
                    fs.Flush();
                    fs.Dispose();
                    fs.Close();
                    jsonContent = jsonData;
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            
            data = JsonMapper.ToObject(jsonContent);
        }

        private void ParseJSON()
        {
            model = new DewityDataModel();

            model.Dewity = new DewityData
            {
                Framework = data["dewity"]["framework"].ToString(),
                Author = data["dewity"]["author"].ToString(),
                Version = data["dewity"]["version"].ToString(),
                Repository = new RepositoryData
                {
                    Type = data["dewity"]["repository"]["type"].ToString(),
                    Url = data["dewity"]["repository"]["url"].ToString()
                }
            };

            model.ManagerConfig = new ManagerConfigData
            {
                GameConsole = bool.Parse(data["managerconfig"]["gameconsole"].ToString())
            };

            //if (model.Dewity.Framework.ToString() != "DewityKit" | model.Dewity.Author.ToString() != "DanKe")
            //{
                //throw new Exception("Dewity 配置文件不合法！");
                //return;
            //}

            //Debug.Log("Dewity 配置文件读取完成，当前版本：" + model.Dewity.Version);
        }

    }
}
    