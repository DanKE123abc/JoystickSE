using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DanKeTools.Event;
using UnityEngine.Events;
using DanKeTools;

namespace JoystickSE
{

    ///<summary>
    ///脚本名称： JoystickSE.cs
    ///修改时间：2022/12/27
    ///脚本功能：JoystickSE接口
    ///备注：
    ///</summary>

    public class JoystickEvent : Singleton<JoystickEvent>
    {
        public void AddListener<Vector2>(UnityAction<Vector2> action)
        {
            EventCenter.instance.AddEventListener<Vector2>("Joystick", action);
        }
        
    }

}
