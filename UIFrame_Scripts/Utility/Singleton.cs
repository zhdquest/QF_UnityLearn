using System;

namespace Utilty
{
    public class Singleton<T>
    {
        //静态单例
        private static T instance;
        //获取单例
        public static T GetInstance()
        {
            if (instance == null)
            {
                instance = (T)Activator.CreateInstance(typeof(T), true);
            }

            return instance;
        }
    }
}
