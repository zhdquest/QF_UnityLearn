using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utilty;

namespace UIFrame
{
    /// <summary>
    /// UI模块基类，需挂载到模块的根对象上
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    public class UIModuleBase : MonoBehaviour
    {
        /// <summary>
        /// 存储当前模块的所有元件
        /// </summary>
        public Dictionary<string, UIWidgetBase> _uiWidgets;
        //UI模块的控制模式
        public UIModuleMode _uiModuleMode = UIModuleMode.SingleControlMode;
        
        protected CanvasGroup _canvasGroup;

        /// <summary>
        /// 当前模块的元件脚本
        /// </summary>
        [Header("当前模块重要元件所添加的脚本名称")]
        public string currentModuleWidgetScript = "UIFrame.UIWidgetBase";

        protected virtual void Awake()
        {
            _uiWidgets = new Dictionary<string, UIWidgetBase>();
            _canvasGroup = GetComponent<CanvasGroup>();
            LoadImportentWidgetsAndAddLocalization();
            InitModuleLanguage();
        }

        /// <summary>
        /// 初始本地化语言
        /// </summary>
        private void InitModuleLanguage()
        {
            int id = PlayerPrefs.GetInt(SystemDefine.PLAYERPREFS_LANGUAGEID);
            UILocalizationTextManager.
                GetInstance().LocalizationTextChange(id);
        }

        /// <summary>
        /// 绑定控制器
        /// </summary>
        /// <param name="controllerBase"></param>
        protected void BindController(UIControllerBase controllerBase)
        {
            controllerBase.ControllerStart(this);
        }

        /// <summary>
        /// 加载当前模块重要的元件
        /// </summary>
        private void LoadImportentWidgetsAndAddLocalization()
        {
            //判断脚本添加的元件组件是否符合规范
            if (!ReflectionManager.GetInstance().TypeIsExtentOrEquel(
                currentModuleWidgetScript,
                "UIWidgetBase"))
            {
                //命名不规范，依旧使用基类组件
                currentModuleWidgetScript = "UIFrame.UIWidgetBase";
            }

            //遍历当前对象的所有子对象
            Transform[] allChild = transform.GetComponentsInChildren<Transform>();

            //遍历所有的子对象
            for (int i = 0; i < allChild.Length; i++)
            {
                //遍历所有的元件尾部标记符号
                for (int j = 0; j < SystemDefine.IMPORTENT_WIDGET_TOKEN.Length; j++)
                {
                    //判断当前子对象的名字是否以该符号结尾
                    if (allChild[i].name.EndsWith(SystemDefine.IMPORTENT_WIDGET_TOKEN[j]))
                    {
                        //添加该脚本组件
                        UIWidgetBase uiWidget = allChild[i].gameObject.AddComponent(
                            Type.GetType(currentModuleWidgetScript)) as UIWidgetBase;
                        //赋值给脚本所在的模块
                        uiWidget._currentModule = this;
                        //添加到字典
                        _uiWidgets.Add(allChild[i].name,uiWidget);
                        break;
                    }
                }

                //添加本地化组件
                AddLocalizationTextComponent(allChild[i]);
            }
        }

        
        /// <summary>
        /// 添加本地化组件
        /// </summary>
        /// <param name="child"></param>
        private void AddLocalizationTextComponent(Transform child)
        {
            //如果当前子对象，没有Text组件，则跳过
            if(child.GetComponent<Text>() == null)
                return;
            //如果当前对象不参与本地化操作
            if(child.name.StartsWith(SystemDefine.NotInvolvedInLocalization))
                return;
            //给当前对象添加本地化组件
            UILocalText localText = child.gameObject.AddComponent<UILocalText>();
            //设置该本地化脚本的文本标记
            localText.textString = child.GetComponent<Text>().text;
        }

        /// <summary>
        /// 查找当前模块的元件，通过元件名称
        /// </summary>
        /// <param name="widgetName"></param>
        public UIWidgetBase FindCurrentModuleWidget(string widgetName)
        {
            if (!_uiWidgets.ContainsKey(widgetName))
            {
                Debug.LogError("查找的" + widgetName + "不存在!");
                return null;
            }

            //返回元件
            return _uiWidgets[widgetName];
        }

        /// <summary>
        /// 取消注册某一个元件在当前模块
        /// </summary>
        /// <param name="widgetName"></param>
        public void UnRegisterWidget(string widgetName)
        {
            if (!_uiWidgets.ContainsKey(widgetName))
            {
                Debug.LogError("当前模块不存在！");
                return;
            }

            _uiWidgets.Remove(widgetName);

            // Debug.Log(name + "模块中的" + widgetName + "已经取消注册..");
        }

        #region Module Callbacks

        /// <summary>
        /// 当打开当前模块
        /// </summary>
        public virtual void OnOpen()
        {
            //当前模块可以和用户交互
            _canvasGroup.blocksRaycasts = true;
            //将当前对象设置为最后一个子对象，使其显示在所有窗口的上面
            transform.SetSiblingIndex(transform.parent.childCount - 1);
        }

        /// <summary>
        /// 当当前模块被挂起
        /// </summary>
        public virtual void OnPause()
        {
            if (_uiModuleMode == UIModuleMode.SingleControlMode)
            {
                //当前模块不可以和用户交互
                _canvasGroup.blocksRaycasts = false;
            }
        }

        /// <summary>
        /// 当当前模块恢复
        /// </summary>
        public virtual void OnResume()
        {
            if (_uiModuleMode == UIModuleMode.SingleControlMode)
            {
                //当前模块可以和用户交互
                _canvasGroup.blocksRaycasts = true;
            }
        }

        /// <summary>
        /// 当关闭当前模块
        /// </summary>
        public virtual void OnClose()
        {
            //当前模块不可以和用户交互
            _canvasGroup.blocksRaycasts = false;
        }
        #endregion
    }
}

