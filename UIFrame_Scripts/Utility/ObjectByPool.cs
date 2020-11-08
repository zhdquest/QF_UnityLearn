using System;
using UnityEngine;
using System.Collections;

namespace Utilty
{
    public abstract class ObjectByPool : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            // ObjectInit();
        }

        protected virtual void OnDisable()
        {
            // ObjectDispose();
        }

        /// <summary>
        /// 对象从对象池中出来，进行初始化操作
        /// </summary>
        public abstract void ObjectInit();
        
        /// <summary>
        /// 对象进入对象池，进行释放收尾操作
        /// </summary>
        public abstract void ObjectDispose();

    }
}
