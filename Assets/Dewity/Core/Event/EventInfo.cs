using UnityEngine.Events;

namespace Dewity
{


    ///<summary>
    ///脚本名称： EventInfo.cs
    ///修改时间：
    ///脚本功能：
    ///备注：
    ///</summary>

    public interface IEventInfo
    {
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> Actions;

        public EventInfo(UnityAction<T> action)
        {
            Actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction Actions;

        public EventInfo(UnityAction action)
        {
            Actions += action;
        }
    }

}