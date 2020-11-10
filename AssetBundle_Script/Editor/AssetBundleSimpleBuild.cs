using UnityEditor;
using UnityEngine; //引入命名空间
using System.IO;//引入命名空间

public class AssetBundleSimpleBuild : Editor {//继承编辑器类

    //需要打包的资源路径
    public static string assetPath = Application.dataPath + "/NeedBuild";
    //打包后的资源路径
    public static string outputPath = Application.dataPath + "/OutputAssetBundle";
    
    //使用特性设置为菜单项
    //[MenuItem("AssetBundle/SimpleBuildOSX")]
    //创建静态方法【public，无参数】
    public static void SimpleBuildOSX()
    {
        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneOSX);
    }
    
    //使用特性设置为菜单项
    //[MenuItem("AssetBundle/SimpleBuildWindows64")]
    //创建静态方法【public，无参数】
    public static void SimpleBuildWindows64()
    {
        BuildPipeline.BuildAssetBundles(outputPath,
            BuildAssetBundleOptions.ChunkBasedCompression,
            BuildTarget.StandaloneWindows64);
    }


    [MenuItem("AssetBundle/AutoBuildOSX")]
    public static void AutoBuildOSX()
    {
        //清除名称
        ClearAssetBundleNames();
        //设置名称
        SetFilesAssetBundleName(assetPath);
        //打包
        SimpleBuildOSX();
        //清除名称
        ClearAssetBundleNames();
        //刷新资源
        AssetDatabase.Refresh();
    }
    
    [MenuItem("AssetBundle/AutoBuildWindows64")]
    public static void AutoBuildWindows64()
    {
        //清除名称
        ClearAssetBundleNames();
        //设置名称
        SetFilesAssetBundleName(assetPath);
        //打包
        SimpleBuildWindows64();
        //清除名称
        ClearAssetBundleNames();
        //刷新资源
        AssetDatabase.Refresh();
    }

    public static void SetFilesAssetBundleName(string path)
    {
        //获取该路径下的所有文件名称
        string[] filesName = Directory.GetFiles(path);
        //获取该路径下的所有子文件夹名称
        string[] directorysName = Directory.GetDirectories(path);

        for (int i = 0; i < directorysName.Length; i++)
        {
            //递归设置
            SetFilesAssetBundleName(directorysName[i]);
        }

        for (int i = 0; i < filesName.Length; i++)
        {
            //过滤meta文件
            if(filesName[i].EndsWith(".meta"))
                continue;
            //设置该文件的Bundle名称
            SetAssetBundleName(filesName[i]);
        }
    }

    /// <summary>
    /// 设置某个文件的Bundle名称
    /// </summary>
    /// <param name="path"></param>
    private static void SetAssetBundleName(string path)
    {
        //C://abc/def/Lesson01/Assets/Abc/aaa
        
        //C://abc/def/Lesson01/Assets 28
        
        //     /Abc/aaa.prefab
        string relativePath = path.Substring(Application.dataPath.Length);
        //    Assets/Abc/aaa.prefab 得到资源的相对路径
        relativePath = "Assets" + relativePath;

        //声明bundle名称，去掉了前面的路径
        string bundleName = relativePath.Substring(relativePath.LastIndexOf('/') + 1);
        //去掉后缀
        bundleName = bundleName.Remove(bundleName.LastIndexOf('.'));

        //通过相对路径获取资源导入对象
        AssetImporter asset = AssetImporter.GetAtPath(relativePath);

        //设置Bundle名称
        asset.assetBundleName = bundleName;
    }


    /// <summary>
    /// 清除Bundle名称
    /// </summary>
    private static void ClearAssetBundleNames()
    {
        //获取所有的Bundle名称
        string[] names = AssetDatabase.GetAllAssetBundleNames();

        //遍历
        for (int i = 0; i < names.Length; i++)
        {
            //强制移除bundle名称
            AssetDatabase.RemoveAssetBundleName(names[i], true);
        }
    }
}