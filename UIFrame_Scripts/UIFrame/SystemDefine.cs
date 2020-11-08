using UnityEngine;
using System.Collections;

namespace UIFrame
{
    public enum UIModuleMode
    {
        SingleControlMode,//单窗口控制模式
        MultipleControlMode//多窗口控制模式
    }

    /// <summary>
    /// 系统常量
    /// </summary>
    public static class SystemDefine
    {
        /// <summary>
        /// 模块路径配置表
        /// </summary>
        public const string UIPanelConfigPath = "Configuration/ModulePathConfig";
        
        /// <summary>
        /// 模块路径配置表
        /// </summary>
        public const string UITextLocalizationConfigPath = "Configuration/LocalizationTexts";

        /// <summary>
        /// 重要UI元件的末尾标记
        /// </summary>
        public static string[] IMPORTENT_WIDGET_TOKEN;

        /// <summary>
        /// PlayerPrefs的语言ID常数
        /// </summary>
        public const string PLAYERPREFS_LANGUAGEID = "LanguageID";

        
        /// <summary>
        /// 不参与本地化的末尾标记
        /// </summary>
        public const string NotInvolvedInLocalization = "$";

        static SystemDefine()
        {
            IMPORTENT_WIDGET_TOKEN = new [] {"#", "!", "*"};
        }
    }
}

