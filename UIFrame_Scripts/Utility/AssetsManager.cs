using UnityEngine;
using System.Collections.Generic;

///通用框架
namespace Utilty
{
    public class AssetsManager : Singleton<AssetsManager>
    {
        protected AssetsManager()
        {
            assetsCache = new Dictionary<string, Object>();
        }

        //缓存字典
        private Dictionary<string,Object> assetsCache;

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="path"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual T GetAssets<T>(string path) where T : Object
        {
            //先查看缓存池有没有这个资源
            if (assetsCache.ContainsKey(path))
            {
                //直接将缓存中的资源返回
                return assetsCache[path] as T;
            }
            else
            {
                //通过Resouce.Load去加载资源
                T assets = Resources.Load<T>(path);
                //将新资源放在缓存池里
                assetsCache.Add(path,assets);
                //返回资源
                return assets;
            }
        }

        /// <summary>
        /// 卸载未使用的资源
        /// </summary>
        public void UnloadUnuseAssets()
        {
            //卸载
            Resources.UnloadUnusedAssets();
        }
    }

    public class SpriteManager : Singleton<SpriteManager>
    {
        //...
    }

    public class PrefabManager : Singleton<PrefabManager>
    {
        private PrefabManager()
        {
        }

        public GameObject CreateGameObjectByPrefab(string path)
        {
            //获取预设体
            GameObject prefab = AssetsManager.GetInstance().GetAssets<GameObject>(path);
            //生成
            GameObject obj = Object.Instantiate(prefab);
            //返回
            return obj;
        }
        
        public GameObject CreateGameObjectByPrefab(string path,Vector3 pos,Quaternion qua)
        {
            //生成对象
            GameObject obj = CreateGameObjectByPrefab(path);
            //设置坐标和旋转
            obj.transform.position = pos;
            obj.transform.rotation = qua;
            //返回
            return obj;
        }
        
        public GameObject CreateGameObjectByPrefab(string path,Transform parent,Vector3 localPos,Vector3 localScale)
        {
            //生成对象
            GameObject obj = CreateGameObjectByPrefab(path);
            //设置父物体
            obj.transform.SetParent(parent);
            obj.transform.localPosition = localPos;
            obj.transform.localScale = localScale;
            //返回
            return obj;
        }

        public GameObject CreateGameObjectByPrefab(string path,Transform parent,Vector2 anchoredPosition)
        {
            //生成对象
            GameObject obj = CreateGameObjectByPrefab(path);
            //设置父物体
            obj.transform.SetParent(parent);
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RectTransform>().anchoredPosition = anchoredPosition;
            //返回
            return obj;
        }

        public GameObject CreateGameObjectByPrefab(string path,Transform parent,Vector3 localPos,Quaternion localQua)
        {
            //生成对象
            GameObject obj = CreateGameObjectByPrefab(path);
            //设置父物体
            obj.transform.SetParent(parent);
            //设置坐标和旋转
            obj.transform.localPosition = localPos;
            obj.transform.localRotation = localQua;
            //返回
            return obj;
        }
        
        
    }
}
