using System.Collections.Generic;
using UnityEngine;
using Utilty;

namespace UIFrame
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : Singleton<UIManager> {

        private UIManager()
        {
            _uiModules = new Dictionary<string, UIModuleBase>();
            _uiModuleStack = new Stack<UIModuleBase>();
            _canvas = GameObject.FindWithTag("Canvas").transform;
        }
        
        //常用容器型数据结构:
        //Array,ArrayList,List<>,Dictionary<k,v>,HashTable
        //Stack<>,Queue<>,Link(链表),Tree(树)
        
        //频率最高的：List<T>,Dictionary<k,v>

        
        /// <summary>
        /// 全局画布对象
        /// </summary>
        private Transform _canvas;
        /// <summary>
        /// 所管理的所有UI模块
        /// </summary>
        private Dictionary<string, UIModuleBase> _uiModules;
        /// <summary>
        /// UI模块栈
        /// </summary>
        private Stack<UIModuleBase> _uiModuleStack;

        #region Find Module Widget


        /// <summary>
        /// 跨模块获取某个元件【确保要找的模块已经被加载】
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="widgetName"></param>
        /// <returns></returns>
        public UIWidgetBase FindWidget(string moduleName, string widgetName)
        {
            //判断模块名称是否存在
            if (!_uiModules.ContainsKey(moduleName)
            || _uiModules[moduleName] == null)
            {
                Debug.LogError("该模块不存在");
                return null;
            }
            
            //通过模块去获取相应的元件
            return _uiModules[moduleName].FindCurrentModuleWidget(widgetName);
        }


        #endregion
        
        #region Load / Unload 模块

        /// <summary>
        /// 加载模块
        /// </summary>
        /// <param name="moduleName"></param>
        private void LoadModule(string moduleName,Vector2 anchoredPosition)
        {
            //如果字典中根本没有该模块,或字典里该模块的值为空
            if (!_uiModules.ContainsKey(moduleName)
                || _uiModules[moduleName] == null)
            {
                //通过模块名称获取对应的资源路径
                string panelPath = UIConfigurationManager.GetInstance().GetPanelPrefabPathByName(moduleName);
                //加载预设体，并生成对应的对象
                GameObject module = PrefabManager.GetInstance().CreateGameObjectByPrefab(panelPath,
                    _canvas, anchoredPosition);
                //修改模块的名称，去掉(Clone)
                module.name = moduleName;
                //获取模块组件
                UIModuleBase moduleBase = module.GetComponent<UIModuleBase>();
                //添加到字典里
                _uiModules[moduleName] = moduleBase;
            }
        }

        /// <summary>
        /// 卸载模块
        /// </summary>
        /// <param name="moduleName"></param>
        private void UnloadModule(string moduleName)
        {
            if (!_uiModules.ContainsKey(moduleName))
            {
                Debug.LogError("当前模块不存在，无法卸载...");
                return;
            }

            //获取该模块
            UIModuleBase moduleBase = _uiModules[moduleName];
            //从字典里移除
            _uiModules.Remove(moduleName);
            //将该对象销毁
            Object.Destroy(moduleBase.gameObject);
        }

        #endregion

        #region 单窗口模式的栈型管理

        /// <summary>
        /// 将一个模块压栈
        /// </summary>
        public void PushModule(string moduleName)
        {
            //动态加载该模块
            LoadModule(moduleName,Vector2.zero);

            if (_uiModules[moduleName]._uiModuleMode 
                == UIModuleMode.MultipleControlMode)
            {
                Debug.LogError("当前窗口为多窗口模式，请使用OpenModule方法打开！");
                UnloadModule(moduleName);
                return;
            }

            //判断栈内是否有模块存在
            if (_uiModuleStack.Count != 0)
            {
                //让栈顶模块执行暂停方法
                _uiModuleStack.Peek().OnPause();
            }
            
            //将当前加载的模块压栈
            _uiModuleStack.Push(_uiModules[moduleName]);
            //此时该模块执行打开方法
            _uiModuleStack.Peek().OnOpen();
        }

        /// <summary>
        /// 让栈顶模块出栈
        /// </summary>
        public void PopModule()
        {
            //如果栈为空
            if(_uiModuleStack.Count == 0)
                return;
            //栈顶模块出栈
            _uiModuleStack.Pop().OnClose();
            //如果栈为空
            if(_uiModuleStack.Count == 0)
                return;
            //新的栈顶模块执行恢复方法
            _uiModuleStack.Peek().OnResume();
        }
        
        #endregion

        #region 多窗口模式的模块管理

        /// <summary>
        /// 打开多窗口模式的模块
        /// </summary>
        /// <param name="moduleName"></param>
        /// <param name="anchoredPosition"></param>
        public void OpenModule(string moduleName,Vector2 anchoredPosition)
        {
            //加载模块
            LoadModule(moduleName,anchoredPosition);
            
            if (_uiModules[moduleName]._uiModuleMode 
                == UIModuleMode.SingleControlMode)
            {
                Debug.LogError("当前窗口为单窗口模式，请使用PushModule方法打开！");
                UnloadModule(moduleName);
                return;
            }
            
            //执行打开方法
            _uiModules[moduleName].OnOpen();
        }

        /// <summary>
        /// 关闭多窗口模式的模块
        /// </summary>
        /// <param name="moduleName"></param>
        public void CloseModule(string moduleName)
        {
            if (!_uiModules.ContainsKey(moduleName))
            {
                Debug.LogError("当前模块不存在，无法关闭...");
                return;
            }
            
            //执行关闭
            _uiModules[moduleName].OnClose();
        }

        #endregion
    }
}
