using System.Collections.Generic;
using UnityEngine;

namespace Utilty
{
    public class ObjectPool : Singleton<ObjectPool> {
        
        //私有构造
        private ObjectPool()
        {
            pool = new Dictionary<string, List<GameObject>>();
        }

        /// <summary>
        /// 对象池
        /// </summary>
        private Dictionary<string,List<GameObject>> pool;
        
        /// <summary>
        /// 通过对象池生成游戏对象
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GameObject SpawnObject(string name)
        {
            //需要的对象
            GameObject needObj;
            
            //查看是否有该名字所对应的子池，且子池中有对象
            if (pool.ContainsKey(name) && pool[name].Count > 0)
            {
                //将0号对象返回
                needObj = pool[name][0];
                //将0号对象从List中移除
                pool[name].RemoveAt(0);
            }
            else
            {
                //直接通过Instantiate生成
                needObj = PrefabManager.GetInstance().CreateGameObjectByPrefab(name);
                //修改名称，去掉(Clone)
                needObj.name = name;
            }
            //设置为激活
            needObj.SetActive(true);
            
            //执行初始化操作
            needObj.GetComponent<ObjectByPool>().ObjectInit();
            
            //返回
            return needObj;
        }

        /// <summary>
        /// 回收游戏对象到对象池
        /// </summary>
        /// <param name="obj"></param>
        public void RecycleObj(GameObject obj)
        {
            //防止被看到，设置为非激活
            obj.SetActive(false);
            //如果有对相应的子池
            if (pool.ContainsKey(obj.name))
            {
                //将当前对象放入子池
                pool[obj.name].Add(obj);
            }
            else
            {
                //创建该子池，并将对象放入
                pool.Add(obj.name, new List<GameObject>{obj});
            }
            
            //执行收尾操作
            obj.GetComponent<ObjectByPool>().ObjectDispose();
        }
    }
}
