using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace UIFrame
{
    /// <summary>
    /// UI元件基类
    /// </summary>
    public class UIWidgetBase : MonoBehaviour
    {
        /// <summary>
        /// 当前的模块
        /// </summary>
        public UIModuleBase _currentModule;

        private Transform _transform;
        private RectTransform _rectTransform;
        
        private Text _text;
        private Image _image;
        private RawImage _rawImage;

        private Button _button;
        private InputField _inputField;
        private Slider _slider;
        private Dropdown _dropdown;
        private Toggle _toggle;
        private Scrollbar _scrollbar;
        private SceneView _sceneView;
        private ContentSizeFitter _contentSizeFitter;

        public RectTransform _RectTransform
        {
            get { return _rectTransform; }
        }
        
        public Transform _Transform
        {
            get { return _transform; }
        }
        public Text _Text
        {
            get { return _text; }
        }
        public Image _Image
        {
            get { return _image; }
        }
        public RawImage _RawImage
        {
            get { return _rawImage; }
        }
        public Button _Button
        {
            get { return _button; }
        }
        public InputField _InputField
        {
            get { return _inputField; }
        }
        public Slider _Slider
        {
            get { return _slider; }
        }
        public Dropdown _Dropdown
        {
            get { return _dropdown; }
        }
        public Toggle _Toggle
        {
            get { return _toggle; }
        }
        
        protected virtual void Awake()
        {
            try
            {
                _transform = transform;
                _rectTransform = GetComponent<RectTransform>();
            
                _text = GetComponent<Text>();
                _image = GetComponent<Image>();
                _rawImage = GetComponent<RawImage>();
                _button = GetComponent<Button>();       

                _inputField = GetComponent<InputField>();
                _slider = GetComponent<Slider>();
                _dropdown = GetComponent<Dropdown>();
                _toggle = GetComponent<Toggle>();
                _scrollbar = GetComponent<Scrollbar>();
                _sceneView = GetComponent<SceneView>();
                _contentSizeFitter = GetComponent<ContentSizeFitter>();
            }
            catch (Exception e)
            {
                
            }
        }

        protected virtual void OnDestroy()
        {
            //取消注册
            _currentModule.UnRegisterWidget(name);
        }
    }
}