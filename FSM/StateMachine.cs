
using System;
using System.Collections.Generic;

namespace FSM_Frame
{
    public class StateMachine : State
    {
        #region Constructor

        public StateMachine(string _stateName) : base(_stateName)
        {
            controledStates = new Dictionary<string, State>();
            BindBaseUpdateEvent();
        }

        #endregion

        #region Control States

        /// <summary>
        /// 控制的所有子状态
        /// </summary>
        private Dictionary<string, State> controledStates;

        /// <summary>
        /// 默认状态
        /// </summary>
        private State _defaultState;

        /// <summary>
        /// 当前正在运行的状态
        /// </summary>
        private State _currentState;

        public object EnterParameter { get; set; }
        public object UpdateParameter { get; set; }
        public object ExitParameter { get; set; }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="stateName"></param>
        /// <returns></returns>
        public State AddState(string stateName)
        {
            if (_isRun)
                throw new Exception("状态机正处于运行状态，不允许进行状态的增删..");

            //如果没有包含
            if (!controledStates.ContainsKey(stateName))
            {
                State newState = new State(stateName);
                controledStates.Add(stateName, newState);

                if (controledStates.Count == 1)
                {
                    _defaultState = newState;
                }

                return newState;
            }

            return controledStates[stateName];
        }

        /// <summary>
        /// 添加状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(State state)
        {
            if (_isRun)
                throw new Exception("状态机正处于运行状态，不允许进行状态的增删..");

            if (!controledStates.ContainsKey(state.StateName))
            {
                controledStates.Add(state.StateName, state);
                if (controledStates.Count == 1)
                {
                    //设置默认状态
                    _defaultState = state;
                }
            }
        }

        /// <summary>
        /// 移除状态
        /// </summary>
        /// <param name="stateName"></param>
        /// <exception cref="Exception"></exception>
        public void RemoveState(string stateName)
        {
            if (_isRun)
                throw new Exception("状态机正处于运行状态，不允许进行状态的增删..");
            if (controledStates.ContainsKey(stateName))
            {
                //获取需要删除的状态名称
                string needRemoveName = controledStates[stateName].StateName;
                //移除该状态
                controledStates.Remove(needRemoveName);
                //判断刚刚删除的状态是否是默认状态
                if (needRemoveName == _defaultState.StateName)
                {
                    ChooseNewDefaultState();
                }
            }
        }

        /// <summary>
        /// 选择新的默认状态
        /// </summary>
        private void ChooseNewDefaultState()
        {
            foreach (var item in controledStates)
            {
                _defaultState = item.Value;
                return;
            }

            //如果无子状态，则无默认状态
            _defaultState = null;
        }

        #endregion

        #region EnterState & ExitState 进入 & 离开

        public override void EnterState(object enterParameter, object updateParameter)
        {
            //父类的方法，当前状态机先进入状态
            base.EnterState(enterParameter, updateParameter);

            //当前状态为空，默认状态不为空
            if (_currentState == null)
            {
                if (_defaultState != null)
                {
                    //设置默认状态为当前状态
                    _currentState = _defaultState;
                }
                else
                {
                    return;
                }
            }

            //子状态进入
            _currentState.EnterState(enterParameter, updateParameter);
        }

        public override void ExitState(object exitParameter)
        {
            if (_currentState != null)
            {
                //子状态先离开
                _currentState.ExitState(exitParameter);
            }

            //父状态再离开
            base.ExitState(exitParameter);
        }

        #endregion

        #region Check CurrentState Transition 检查当前状态的过渡

        private void BindBaseUpdateEvent()
        {
            OnStateUpdate += CheckCurrentStateTransition;
        }

        /// <summary>
        /// 检查当前状态的过渡
        /// </summary>
        private void CheckCurrentStateTransition(object obj)
        {
            if (_currentState != null)
            {
                //获取当前状态所能切换的状态条件是否有能够满足的
                string targetState = _currentState.CheckStateTransition();
                
                if(targetState == null)
                    return;
                //状态过渡
                Transition(targetState);
            }
        }

        /// <summary>
        /// 过渡状态
        /// </summary>
        /// <param name="stateName"></param>
        private void Transition(string stateName)
        {
            //待过渡的状态必须是当前状态机的子状态，否则不予执行
            if(!controledStates.ContainsKey(stateName))
                throw new Exception("待过渡的状态必须是当前状态机的子状态，否则不予执行...");
            
            //旧的当前状态，离开
            _currentState.ExitState(ExitParameter);
            
            //设置新的当前状态[更新]
            _currentState = controledStates[stateName];
            
            //新的当前状态，进入
            _currentState.EnterState(EnterParameter,UpdateParameter);
        }

        #endregion
        
    }
}