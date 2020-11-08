using System;
using UnityEngine;
using System.Collections;
using Utilty;

namespace UIFrame
{
    public class UILocalizationTextManager : Singleton<UILocalizationTextManager> {

        private UILocalizationTextManager()
        {
        }

        private event Action<int> changeLanguageAction;

        public void AddTextActionListener(Action<int> action)
        {
            changeLanguageAction += action;
        }

        public void RemoveTextActionListener(Action<int> action)
        {
            changeLanguageAction -= action;
        }

        public void LocalizationTextChange(int languageID)
        {
            //将语言ID存储到本地
            PlayerPrefs.SetInt(SystemDefine.PLAYERPREFS_LANGUAGEID,languageID);
            
            if (changeLanguageAction != null)
            {
                //更换所有绑定了的文本的语言
                changeLanguageAction(languageID);
            }
        }
    }
}

