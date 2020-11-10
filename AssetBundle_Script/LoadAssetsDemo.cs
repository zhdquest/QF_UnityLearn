using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class LoadAssetsDemo : MonoBehaviour
{
    [Header("版本号")]
    public int version = 1;
    
    [Header("加载本地资源")]
    public bool loadLocal = true;
    [Header("资源的bundle名称")]
    public string assetBundleName;
    [Header("资源的真正的文件名称")]
    public string assetRealName;
    
    //bundle所在的路径
    private string assetBundlePath;
    //bundle所在的文件夹名称
    private string assetBundleRootName;

    private void Awake()
    {
        assetBundlePath = Application.dataPath + "/OutputAssetBundle";
        assetBundleRootName = assetBundlePath.Substring(assetBundlePath.LastIndexOf("/") + 1);
        
        Debug.Log(assetBundleRootName);
    }

    private void Start()
    {
        // StartCoroutine(LoadNoDepandenceAsset());
        StartCoroutine(LoadAssetsByWWW());
        // StartCoroutine(LoadAssetsByFile());
        // StartCoroutine(LoadAssetsByMemory());
        // StartCoroutine(LoadAssetsByStream());
        // StartCoroutine(LoadAssetsByWebRequest());
    }

    private IEnumerator LoadToLocal()
    {
        WWW www = new WWW("www.baidu.com/abc");

        yield return www;

        // www.assetBundle.LoadAsset<GameObject>("");
        
        File.WriteAllBytes("",www.bytes);
    }

    private IEnumerator LoadAssetsByWWW()
    {
        string path = "";
        
        if (loadLocal)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            path += "File:///";
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            path += "File://";
#endif
        }

        //获取要加载的资源路径【bundle总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        //加载
        WWW www = WWW.LoadFromCacheOrDownload(path, version);
        //等待下载
        yield return www;
        //获取其中的bundle
        AssetBundle manifestBundle = www.assetBundle;
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);

        //卸载Bundle和解压出来的Manifest对象
        manifestBundle.Unload(true);
        
        //获取到相对路径  file:///user/..../HLLesson11/Assets/Output/【Output】
        path = path.Remove(path.LastIndexOf("/")+1);
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            //file:///user/..../HLLesson11/Assets/Output/mat
            string depPath = path + dependencies[i];
            //获取新的路径进行加载
            www = WWW.LoadFromCacheOrDownload(depPath, version);
            //等待下载
            yield return www;
            //将依赖临时保存
            depAssetBundles[i] = www.assetBundle;
        }
        
        //获取路径
        path += assetBundleName;
        //加载终极资源
        www = WWW.LoadFromCacheOrDownload(path, version);
        //等待下载
        yield return www;
        //获取到资源的Bundle
        AssetBundle realAssetBundle = www.assetBundle;
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        //TEST:生成
        Instantiate(prefab);
        
        //卸载依赖
        for (int i = 0; i < depAssetBundles.Length; i++)
        {
            depAssetBundles[i].Unload(false);
        }
        
        //卸载主资源Bundle
        realAssetBundle.Unload(true);
    }
    
    private IEnumerator LoadAssetsByFile()
    {
        string path = "";
        //获取要加载的资源路径【bundle总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        //获取其中的bundle
        AssetBundle manifestBundle = AssetBundle.LoadFromFile(path);
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);

        //获取到相对路径  file:///user/..../HLLesson11/Assets/Output/【Output】
        path = path.Remove(path.LastIndexOf("/")+1);
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            //file:///user/..../HLLesson11/Assets/Output/mat
            string depPath = path + dependencies[i];
            //将依赖临时保存
            depAssetBundles[i] = AssetBundle.LoadFromFile(depPath);
        }
        
        //获取路径
        path += assetBundleName;
        //获取到资源的Bundle
        AssetBundle realAssetBundle = AssetBundle.LoadFromFile(path);
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        //TEST:生成
        Instantiate(prefab);
        
        yield break;
    }
    
    private IEnumerator LoadAssetsByMemory()
    {
        string path = "";
        //获取要加载的资源路径【bundle总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        //获取其中的bundle
        AssetBundle manifestBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);

        //获取到相对路径  file:///user/..../HLLesson11/Assets/Output/【Output】
        path = path.Remove(path.LastIndexOf("/")+1);
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            //file:///user/..../HLLesson11/Assets/Output/mat
            string depPath = path + dependencies[i];
            //将依赖临时保存
            depAssetBundles[i] = AssetBundle.LoadFromMemory(File.ReadAllBytes(depPath));
        }
        
        //获取路径
        path += assetBundleName;
        //获取到资源的Bundle
        AssetBundle realAssetBundle = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        //TEST:生成
        Instantiate(prefab);
        
        yield break;
    }
    
    private IEnumerator LoadAssetsByStream()
    {
        string path = "";
        //获取要加载的资源路径【bundle总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        //获取其中的bundle
        AssetBundle manifestBundle = AssetBundle.LoadFromStream(new FileStream(path, FileMode.Open));
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);

        //获取到相对路径  file:///user/..../HLLesson11/Assets/Output/【Output】
        path = path.Remove(path.LastIndexOf("/")+1);
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            //file:///user/..../HLLesson11/Assets/Output/mat
            string depPath = path + dependencies[i];
            //将依赖临时保存
            depAssetBundles[i] = AssetBundle.LoadFromStream(new FileStream(depPath, FileMode.Open));
        }
        
        //获取路径
        path += assetBundleName;
        //获取到资源的Bundle
        AssetBundle realAssetBundle = AssetBundle.LoadFromStream(new FileStream(path, FileMode.Open));
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        //TEST:生成
        Instantiate(prefab);
        
        yield break;
    }
    
    private IEnumerator LoadAssetsByWebRequest()
    {
        string path = "";
        if (loadLocal)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            path += "File:///";
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            path += "File://";
#endif
        }
        //获取要加载的资源路径【bundle总说明文件】
        path += assetBundlePath + "/" + assetBundleRootName;
        //通过WebRequest的方式获取资源
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(path);
        //等待获取
        yield return request.SendWebRequest();
        //获取其中的bundle
        AssetBundle manifestBundle = DownloadHandlerAssetBundle.GetContent(request);;
        //获取到说明文件
        AssetBundleManifest manifest = manifestBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //获取资源的所有依赖
        string[] dependencies = manifest.GetAllDependencies(assetBundleName);

        //获取到相对路径  file:///user/..../HLLesson11/Assets/Output/【Output】
        path = path.Remove(path.LastIndexOf("/")+1);
        //声明依赖的Bundle数组
        AssetBundle[] depAssetBundles = new AssetBundle[dependencies.Length];
        //遍历加载所有的依赖
        for (int i = 0; i < dependencies.Length; i++)
        {
            //获取到依赖Bundle的路径
            //file:///user/..../HLLesson11/Assets/Output/mat
            string depPath = path + dependencies[i];
            
            request = UnityWebRequestAssetBundle.GetAssetBundle(depPath);
            //等待获取
            yield return request.SendWebRequest();
            //将依赖临时保存
            depAssetBundles[i] = DownloadHandlerAssetBundle.GetContent(request);
        }
        
        //获取路径
        path += assetBundleName;
        
        request = UnityWebRequestAssetBundle.GetAssetBundle(path);
        //等待获取
        yield return request.SendWebRequest();
        //获取到资源的Bundle
        AssetBundle realAssetBundle = DownloadHandlerAssetBundle.GetContent(request);
        //加载真正的资源
        GameObject prefab = realAssetBundle.LoadAsset<GameObject>(assetRealName);
        //TEST:生成
        Instantiate(prefab);
    }

    /// <summary>
    /// 加载无依赖的资源
    /// </summary>
    private IEnumerator LoadNoDepandenceAsset()
    {
        string path = "";
        
        if (loadLocal)
        {
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
            path += "File:///";
#endif
#if UNITY_EDITOR_OSX || UNITY_STANDALONE_OSX
            path += "File://";
#endif
            path += assetBundlePath + "/" + assetBundleName;
            
            //www对象
            WWW www = new WWW(path);

            //等待下载【到内存】
            yield return www;

            //获取到AssetBundle
            AssetBundle bundle = www.assetBundle;

            //加载资源
            GameObject prefab = bundle.LoadAsset<GameObject>(assetRealName);

            //Test:实例化
            Instantiate(prefab);
        }
    }
}