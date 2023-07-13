using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using LitJson;
using UnityEngine;

///<summary>
///脚本名称： ChatApi.cs
///修改时间：
///脚本功能：
///备注：
///</summary>

class ChatApi
{
    private const string api = "https://service-d20dvkuc-1318437443.hk.apigw.tencentcs.com/v1/chat/completions";
    private const string model = "gpt-3.5-turbo";
    private const string api_key = "Bearer";
    List<Dictionary<string, string>> messages = new List<Dictionary<string, string>>
    {
        new Dictionary<string, string>() { { "role", "user" }, { "content", "" } }
    };

    private string AskChatGpt(List<Dictionary<string, string>> messages)
    {
        var data = new Dictionary<string, object>
        {
            { "model", model },
            { "messages", messages },
            { "stream", false }
        };
        var json = JsonMapper.ToJson(data);

        var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Authorization", api_key);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync(api, content).Result;
        var responseJson = response.Content.ReadAsStringAsync().Result;

        //Debug.Log(responseJson);
        var responseDict = JsonMapper.ToObject(responseJson);
        return responseDict["choices"][0]["message"]["content"].ToString();
    }

    public string Get(string content)
    {
        try
        {
            var d = new Dictionary<string, string>() { { "role", "user" }, { "content", content } };
            messages.Add(d);

            content = AskChatGpt(messages);

            d = new Dictionary<string, string>() { { "role", "assistant" }, { "content", content } };
            messages.Add(d);
            return "AI:     " + content;
        }
        catch (Exception ex)
        {
            messages.RemoveAt(messages.Count - 1);
            return "AI:     出现错误，请在DewityKit社区反馈问题：https://github.com/DanKE123abc/DewityKit/issues" +
                   "\r\n\r\n错误报告：" + ex;
        }
    }
}