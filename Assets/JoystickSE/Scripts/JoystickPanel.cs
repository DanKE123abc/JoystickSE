using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dewity;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace JoystickSE.Code
{


    ///<summary>
    ///脚本名称： JoystickPanel.cs
    ///修改时间：2022/12/27
    ///脚本功能：
    ///备注：
    ///</summary>


    public enum JoystickType
    {
        Normal,
        DefaultHide,
        Move,
    }
    
    public class JoystickPanel : UIBasePanel
    {
        public JoystickType type = JoystickType.Normal;
        public float maxL = 180;
        private Image TouchRect;
        private Image BK;
        private Image Control;

        void Start()
        {
            BK = GetControl<Image>("BackGround");
            TouchRect = GetControl<Image>("TouchRect");
            Control = GetControl<Image>("Controller");

            UITriggerManager.instance.AddTriggersListener(TouchRect, UIEventTriggerType.PointerDown, PointerDown);
            UITriggerManager.instance.AddTriggersListener(TouchRect, UIEventTriggerType.PointerUp, PointerUp);
            UITriggerManager.instance.AddTriggersListener(TouchRect, UIEventTriggerType.Drag, Drag);

            if (type != JoystickType.Normal)
            {
                BK.gameObject.SetActive(false);
            }
        }

        public void PointerDown(BaseEventData data)
        {
            BK.gameObject.SetActive(true);
            
            if (type != JoystickType.Normal)
            {
                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    TouchRect.rectTransform, //父对象
                    (data as PointerEventData).position, //鼠标位置
                    (data as PointerEventData).pressEventCamera, //摄像机
                    out localPos); //相对坐标

                BK.transform.localPosition = localPos;
            }
            
        }

        public void PointerUp(BaseEventData data)
        {
            Control.transform.localPosition = Vector3.zero;
            EventCenter.instance.EventTrigger<Vector2>("Joystick", Vector2.zero);
            
            if (type != JoystickType.Normal)
            {
                BK.gameObject.SetActive(false);
            }
        }

        public void Drag(BaseEventData data)
        {

            Vector2 localPos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                BK.rectTransform, //父对象
                (data as PointerEventData).position, //鼠标位置
                (data as PointerEventData).pressEventCamera, //摄像机
                out localPos); //相对坐标

            Control.transform.localPosition = localPos;

            if (localPos.magnitude > 180)
            {
                if (type == JoystickType.Move)
                {
                    BK.transform.localPosition += (Vector3)(localPos.normalized * (localPos.magnitude - maxL));
                }
                Control.transform.localPosition = localPos.normalized * 180;
            }

            EventCenter.instance.EventTrigger<Vector2>("Joystick", localPos.normalized);

        }


    }

}
