using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace UIFrame
{
    [RequireComponent(typeof(Text))]
    public class UILocalText : MonoBehaviour
    {
        private Text _text;
        
        [Header("当前文本的字符串名称「英文」")]
        public string textString;

        private void Awake()
        {
            _text = GetComponent<Text>();
        }

        private void OnEnable()
        {
            //添加更换语言的监听
            UILocalizationTextManager.GetInstance().
                AddTextActionListener(ChangeLanguage);
        }

        private void OnDisable()
        {
            //移除更换语言的监听
            UILocalizationTextManager.GetInstance().
                RemoveTextActionListener(ChangeLanguage);
        }

        /// <summary>
        /// 更换语言
        /// </summary>
        /// <param name="languageID"></param>
        private void ChangeLanguage(int languageID)
        {
            //更换语言，设置文本
            _text.text = UIConfigurationManager.GetInstance().
                GetTextStringByLanguageID(
                    textString, languageID);
        }
    }
}