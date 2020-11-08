using System;
using UnityEngine;

namespace Utilty
{
    public class ReflectionManager : Singleton<ReflectionManager> {

        private ReflectionManager()
        {
        }

        /// <summary>
        /// 当前程序集下的某个类是否存在
        /// </summary>
        /// <param name="typeStr"></param>
        /// <returns></returns>
        private bool TypeIsExist(string typeStr)
        {
            Type type = Type.GetType(typeStr);
            return type != null;
        }

        /// <summary>
        /// 判断某个类是否继承另一个类
        /// </summary>
        /// <param name="typeStr"></param>
        /// <param name="extentType"></param>
        /// <returns></returns>
        public bool TypeIsExtentOrEquel(string typeStr, string extentType)
        {
            //判断类型是否都存在
            if (!TypeIsExist(typeStr))
                return false;
            if (typeStr == extentType)
                return true;
            //判断父类的名字是否和参数匹配
            return Type.GetType(typeStr).BaseType.Name == extentType;
        }
    }
}