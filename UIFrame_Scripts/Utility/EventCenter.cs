using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utilty
{
    public enum EventType
    {
        GetTask = 1,
        BuyEquip
    }

    /// <summary>
    /// 事件中心
    /// </summary>
    public class EventCenter : Singleton<EventCenter>
    {
        private EventCenter()
        {
            allEvents = new Dictionary<EventType, Delegate>();
        }

        /// <summary>
        /// 消息中心的所有事件
        /// </summary>
        private Dictionary<EventType, Delegate> allEvents;

        #region Event Center AddListener

        /// <summary>
        /// 当添加监听时做的检测
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        private void OnAddListener(EventType eventType, Delegate action)
        {
            //如果没有该事件类型
            if (!allEvents.ContainsKey(eventType))
            {
                allEvents.Add(eventType,null);
            }
            //如果该事件已经添加了监听，新的监听事件和已有监听事件，同类型
            else if(allEvents[eventType].GetType() != action.GetType())
            {
                Debug.LogError("添加监听的类型与事件的类型不匹配..【消息类型为："
                               + allEvents[eventType].GetType() + 
                               "】，【添加的监听类型为：" + 
                               action.GetType()+"】");
            }
        }

        /// <summary>
        /// 在事件中心添加【无参数】无返回值的委托监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void AddListener(EventType eventType, Action action)
        {
            //添加监听准备
            OnAddListener(eventType, action);
            //添加新的监听事件
            allEvents[eventType] = (Action)allEvents[eventType] + action;
        }

        /// <summary>
        /// 在事件中心添加【1个参数】无返回值的委托监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void AddListener<T>(EventType eventType, Action<T> action)
        {
            //添加监听准备
            OnAddListener(eventType, action);
            //添加新的监听事件
            allEvents[eventType] = (Action<T>)allEvents[eventType] + action;
        }
        
        /// <summary>
        /// 在事件中心添加【2个参数】无返回值的委托监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void AddListener<T,X>(EventType eventType, Action<T,X> action)
        {
            //添加监听准备
            OnAddListener(eventType, action);
            //添加新的监听事件
            allEvents[eventType] = (Action<T,X>)allEvents[eventType] + action;
        }
        
        /// <summary>
        /// 在事件中心添加【3个参数】无返回值的委托监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void AddListener<T,X,Y>(EventType eventType, Action<T,X,Y> action)
        {
            //添加监听准备
            OnAddListener(eventType, action);
            //添加新的监听事件
            allEvents[eventType] = (Action<T,X,Y>)allEvents[eventType] + action;
        }
        
        /// <summary>
        /// 在事件中心添加【4个参数】无返回值的委托监听
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public void AddListener<T,X,Y,Z>(EventType eventType, Action<T,X,Y,Z> action)
        {
            //添加监听准备
            OnAddListener(eventType, action);
            //添加新的监听事件
            allEvents[eventType] = (Action<T,X,Y,Z>)allEvents[eventType] + action;
        }

        #endregion

        #region Event Center RemoveListener

        /// <summary>
        /// 当移除监听时做的检测
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        private void OnRemoveListener(EventType eventType, Delegate action)
        {
            if (!allEvents.ContainsKey(eventType))
            {
                Debug.LogError("没有该类型的事件存在..." + eventType);
            }
            else if(allEvents[eventType] == null)
            {
                Debug.LogError("该类型的事件为空，无法移除..." + eventType);
            }
            else if(allEvents[eventType].GetType() != action.GetType())
            {
                Debug.LogError("传入的事件类型与消息类型不匹配..." + 
                               allEvents[eventType].GetType() + "..." +
                               action.GetType());
            }
        }

        /// <summary>
        /// 移除【0参数】的监听事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener(EventType eventType, Action action)
        {
            //移除监听的检测
            OnRemoveListener(eventType, action);
            //移除监听
            allEvents[eventType] = (Action)allEvents[eventType] - action;
        }
        
        /// <summary>
        /// 移除【1参数】的监听事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener<T>(EventType eventType, Action<T> action)
        {
            //移除监听的检测
            OnRemoveListener(eventType, action);
            //移除监听
            allEvents[eventType] = (Action<T>)allEvents[eventType] - action;
        }
        
        /// <summary>
        /// 移除【2参数】的监听事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener<T,X>(EventType eventType, Action<T,X> action)
        {
            //移除监听的检测
            OnRemoveListener(eventType, action);
            //移除监听
            allEvents[eventType] = (Action<T,X>)allEvents[eventType] - action;
        }
        
        /// <summary>
        /// 移除【3参数】的监听事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener<T,X,Y>(EventType eventType, Action<T,X,Y> action)
        {
            //移除监听的检测
            OnRemoveListener(eventType, action);
            //移除监听
            allEvents[eventType] = (Action<T,X,Y>)allEvents[eventType] - action;
        }
        
        /// <summary>
        /// 移除【4参数】的监听事件
        /// </summary>
        /// <param name="eventType"></param>
        /// <param name="action"></param>
        public void RemoveListener<T,X,Y,Z>(EventType eventType, Action<T,X,Y,Z> action)
        {
            //移除监听的检测
            OnRemoveListener(eventType, action);
            //移除监听
            allEvents[eventType] = (Action<T,X,Y,Z>)allEvents[eventType] - action;
        }

        #endregion

        #region Event Center RemoveAllListeners

        /// <summary>
        /// 移除该类型的所有事件监听
        /// </summary>
        /// <param name="eventType"></param>
        public void RemoveAllListeners(EventType eventType)
        {
            if (!allEvents.ContainsKey(eventType))
            {
                Debug.LogError("移除监听的事件不存在...");
            }
            else
            {
                //从字典移除该监听类型
                allEvents.Remove(eventType);
            }
        }

        #endregion

        #region Event Center CallEvent

        /// <summary>
        /// 调用前的检测
        /// </summary>
        /// <param name="eventType"></param>
        private void OnCall(EventType eventType)
        {
            if (!allEvents.ContainsKey(eventType) ||
                allEvents[eventType] == null)
            {
                Debug.LogError("事件类型不存在或为空，无法调用...");
            }
        }

        /// <summary>
        /// 调用事件中心的事件【无参数】
        /// </summary>
        /// <param name="eventType"></param>
        public void Call(EventType eventType)
        {
            //调用前的检测
            OnCall(eventType);
            //获取该事件
            Action action = (Action) allEvents[eventType];
            //调用该事件
            action();
        }
        
        /// <summary>
        /// 调用事件中心的事件【1参数】
        /// </summary>
        /// <param name="eventType"></param>
        public void Call<T>(EventType eventType,T arg1)
        {
            //调用前的检测
            OnCall(eventType);
            //获取该事件
            Action<T> action = (Action<T>) allEvents[eventType];
            //调用该事件
            action(arg1);
        }
        
        /// <summary>
        /// 调用事件中心的事件【2参数】
        /// </summary>
        /// <param name="eventType"></param>
        public void Call<T,X>(EventType eventType,T arg1,X arg2)
        {
            //调用前的检测
            OnCall(eventType);
            //获取该事件
            Action<T,X> action = (Action<T,X>) allEvents[eventType];
            //调用该事件
            action(arg1,arg2);
        }
        
        /// <summary>
        /// 调用事件中心的事件【3参数】
        /// </summary>
        /// <param name="eventType"></param>
        public void Call<T,X,Y>(EventType eventType,T arg1,X arg2,Y arg3)
        {
            //调用前的检测
            OnCall(eventType);
            //获取该事件
            Action<T,X,Y> action = (Action<T,X,Y>) allEvents[eventType];
            //调用该事件
            action(arg1,arg2,arg3);
        }
        
        /// <summary>
        /// 调用事件中心的事件【4参数】
        /// </summary>
        /// <param name="eventType"></param>
        public void Call<T,X,Y,Z>(EventType eventType,T arg1,X arg2,Y arg3,Z arg4)
        {
            //调用前的检测
            OnCall(eventType);
            //获取该事件
            Action<T,X,Y,Z> action = (Action<T,X,Y,Z>) allEvents[eventType];
            //调用该事件
            action(arg1,arg2,arg3,arg4);
        }

        #endregion
    }
}