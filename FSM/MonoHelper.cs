using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace FSM_Frame
{
    public class MonoHelper : MonoBehaviour
    {
        
        //内部类[更新事件+更新事件参数]
        private class UpdateEventMsg
        {
            public Action<object> updateEvent;
            public object updateParameters;

            public UpdateEventMsg(Action<object> updateEvent, object updateParameters)
            {
                this.updateEvent = updateEvent;
                this.updateParameters = updateParameters;
            }
        }
        
        
        public static MonoHelper instance;
        
        //执行的时间间隔
        public float invokeInterval = 0;
        
        /// <summary>
        /// 被管理的状态更新事件
        /// </summary>
        private Dictionary<string, UpdateEventMsg> controledStateUpdateEvents;

        /// <summary>
        /// 被管理的状态【数组版】
        /// </summary>
        private UpdateEventMsg[] controledStateMsgArray;

        private void Awake()
        {
            instance = this;
            controledStateUpdateEvents = new Dictionary<string, UpdateEventMsg>();
        }

        /// <summary>
        /// 字典数据导入数组中
        /// </summary>
        private void DicToArray()
        {
            //下标
            int index = 0;
            //遍历字典
            foreach (var item in controledStateUpdateEvents)
            {
                controledStateMsgArray[index] = item.Value;
                index++;
            }
        }

        #region Add / Remove UpdateEvent

        /// <summary>
        /// 添加更新事件
        /// </summary>
        /// <param name="updateEvent"></param>
        /// <param name="parameter"></param>
        public void AddUpdateEvent(string stateName,Action<object> updateEvent, object parameter = null)
        {
            //添加事件函数
            if (!controledStateUpdateEvents.ContainsKey(stateName))
            {
                //添加更新事件
                controledStateUpdateEvents.Add(stateName,new UpdateEventMsg(updateEvent,parameter));
                
                //实例化数组
                controledStateMsgArray = new UpdateEventMsg[controledStateUpdateEvents.Count];
                //导入
                DicToArray();
            }
        }

        /// <summary>
        /// 移除更新事件
        /// </summary>
        /// <param name="updateEvent"></param>
        public void RemoveUpdateEvent(string stateName)
        {
            if (controledStateUpdateEvents.ContainsKey(stateName))
            {
                //从字典中移除
                controledStateUpdateEvents.Remove(stateName);
                
                //实例化数组
                controledStateMsgArray = new UpdateEventMsg[controledStateUpdateEvents.Count];
                //导入
                DicToArray();
            }
        }

        #endregion
        
        private IEnumerator Start()
        {
            while (true)
            {
                if (controledStateUpdateEvents != null)
                {
                    //调用事件
                    for (int i = 0; i < controledStateMsgArray.Length; i++)
                    {
                        //调用
                        controledStateMsgArray[i].updateEvent(
                            controledStateMsgArray[i].updateParameters);
                    }
                }

                if (invokeInterval <= 0)
                {
                    //等一帧
                    yield return null;
                }
                else
                {
                    //等一次该时间间隔
                    yield return new WaitForSeconds(invokeInterval);
                }
            }
        }
    }
    
}