using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM_Frame
{
    public class State
    {
        #region Constructor 构造器

        public State(string _stateName)
        {
            this._stateName = _stateName;
            _transitionDic = new Dictionary<string, Func<bool>>();
            BindBaseStateEvents();
        }

        #endregion
        
        #region State Field 字段

        //状态名称
        protected string _stateName; 
        //状态是否正在运行
        protected bool _isRun;

        #endregion

        #region State Properties 属性

        public string StateName
        {
            get { return _stateName; }
            set { _stateName = value; }
        }

        public bool IsRun
        {
            get { return _isRun; }
            set { _isRun = value; }
        }

        #endregion

        #region State Transition 过渡

        /// <summary>
        /// 过渡字典
        /// </summary>
        private Dictionary<string, Func<bool>> _transitionDic;

        /// <summary>
        /// 注册新的状态过渡
        /// </summary>
        /// <param name="transitionStateName">过渡状态名称</param>
        /// <param name="transitionCondition">过渡条件</param>
        public void RegisterStateTransition(string transitionStateName,Func<bool> transitionCondition)
        {
            //如果过渡条件为空
            if (transitionCondition == null)
            {
                throw new Exception("注册的状态过渡条件为空...");
            }

            //添加、更新
            _transitionDic[transitionStateName] = transitionCondition;
        }
        
        /// <summary>
        /// 取消注册状态过渡
        /// </summary>
        /// <param name="transitionStateName"></param>
        public void UnRegisterStateTransition(string transitionStateName)
        {
            if (!_transitionDic.ContainsKey(transitionStateName))
            {
                throw new Exception("状态列表中无此状态，无法取消注册...");
            }

            //移除
            _transitionDic.Remove(transitionStateName);
        }



        #endregion

        #region State Events 事件

        public event Action<object> OnStateEnter;
        public event Action<object> OnStateUpdate;
        public event Action<object> OnStateExit;

        #endregion

        #region Bind Base State Events 基础状态事件

        private void BindBaseStateEvents()
        {
            OnStateEnter += (obj) => _isRun = true;
            OnStateExit += (obj) => _isRun = false;
        }

        #endregion

        #region EnterState & ExitState 进入 & 离开
        
        /// <summary>
        /// 进入当前状态
        /// </summary>
        /// <param name="enterParameter"></param>
        /// <param name="updateParameter"></param>
        public virtual void EnterState(object enterParameter, object updateParameter)
        {
            if (OnStateEnter != null)
            {
                //执行状态进入事件
                OnStateEnter(enterParameter);
            }

            if (OnStateUpdate != null)
            {
                //将当前的更新事件添加去持续执行
                MonoHelper.instance.AddUpdateEvent(_stateName,OnStateUpdate,updateParameter);
            }
        }

        /// <summary>
        /// 离开状态
        /// </summary>
        /// <param name="exitParameter"></param>
        public virtual void ExitState(object exitParameter)
        {
            //将当前的更新事件取消持续执行
            MonoHelper.instance.RemoveUpdateEvent(_stateName);

            if (OnStateExit != null)
            {
                //执行离开事件
                OnStateExit(exitParameter);
            }
        }

        #endregion

        #region Check State Transition 检查是否需要过渡

        /// <summary>
        /// 检查是否需要过渡
        /// </summary>
        /// <returns></returns>
        public string CheckStateTransition()
        {
            foreach (var item in _transitionDic)
            {
                //判断过渡到目标状态的条件是否达成
                if (item.Value())
                {
                    return item.Key;
                }
            }

            return null;
        }

        #endregion
        
    }
}