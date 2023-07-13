using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FindRepeatAssetsOutput
{

    ///<summary>
    ///脚本名称： FindRepeatAssetsOutput.cs
    ///修改时间：2023/2/25
    ///脚本功能：打印炫酷的Log信息
    ///备注：
    ///</summary>
    
    public class Output
    {
        public static bool EnableLog = true;

        public static void DrawLine(Vector3 start, Vector3 end, Color color, float duration)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end, color, duration);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end);
            }
        }

        public static void DrawLine(Vector3 start, Vector3 end, Color color)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.DrawLine(start, end, color);
            }
        }

        public static void Log(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.Log(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, message), context);
            }
        }

        public static void Log(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.Log(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, message));
            }
        }

        public static void LogAssertion(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogAssertion(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message));
            }
        }

        public static void LogAssertionFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogAssertionFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    format), args);
            }
        }

        public static void LogError(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogError(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message), context);
            }
        }

        public static void LogError(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogError(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    message));
            }
        }

        public static void LogErrorFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogErrorFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 13,
                    format), args);
            }
        }

        public static void LogException(Exception exception)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogException(new Exception(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "red", 12,
                    exception)));
            }
        }

        public static void LogFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "#DDDDDD", 13, format), args);
            }
        }

        public static void LogWarning(object message)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarning(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    message));
            }
        }

        public static void LogWarning(object message, UnityEngine.Object context)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarning(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    message), context);
            }
        }

        public static void LogWarningFormat(string format, params object[] args)
        {
            if (EnableLog)
            {
                UnityEngine.Debug.LogWarningFormat(string.Format("<b><color={0}><size={1}>{2}</size></color></b>", "yellow", 13,
                    format), args);
            }
        }
    }
    public class font
    {
        /// <summary>
        /// 自定义字体大小
        /// </summary>
        /// <param name="size">字体大小</param>
        /// <returns></returns>
        public static string size(int size,string msg)
        {
            msg = "<size=" + size + ">" + msg + "</size>";
            return msg;
        }


        /// <summary>
        /// 自定义颜色
        /// </summary>
        /// <param name="color">十六进制颜色</param>
        /// <returns></returns>
        public static string color(string color,string msg){
            msg = "<color=" + color + ">" + msg + "</color>";
            return msg;
        }
    
        /// <summary>
        /// 内置颜色
        /// </summary>
        public class _color{
            public static string black(string msg){
                msg = "<color=#FF000000>" + msg + "</color>";
                return msg;
            }
            public static string white(string msg){
                msg = "<color=#FFFFFFFF>" + msg + "</color>";
                return msg;
            }
            public static string red(string msg){
                msg = "<color=#FF0000>" + msg + "</color>";
                return msg;
            }
            public static string orange(string msg){
                msg = "<color=#FF7F00>" + msg + "</color>";
                return msg;
            }
            public static string yellow(string msg){
                msg = "<color=#FFFF00>" + msg + "</color>";
                return msg;
            }
            public static string green(string msg){
                msg = "<color=#00FF00>" + msg + "</color>";
                return msg;
            }
            public static string cyan(string msg){
                msg = "<color=#00FFFF>" + msg + "</color>";
                return msg;
            }
            public static string blue(string msg){
                msg = "<color=#0000FF>" + msg + "</color>";
                return msg;
            }
            public static string purple(string msg){
                msg = "<color=#8B00FF>" + msg + "</color>";
                return msg;
            }
        }

    }
}
