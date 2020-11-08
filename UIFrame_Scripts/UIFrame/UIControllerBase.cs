using UnityEngine;

namespace UIFrame
{
    public class UIControllerBase
    {
        protected UIModuleBase _module;

        /// <summary>
        /// 控制器启动
        /// </summary>
        /// <param name="module"></param>
        public virtual void ControllerStart(UIModuleBase module)
        {
            _module = module;
        }
    }
}