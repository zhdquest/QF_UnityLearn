using System;
using UnityEngine;

public class DemoFrame
{
    // [Obsolete]
    // [Obsolete("当前方法过时了，请使用NewDemoShowMe来替换它")]
    [Obsolete("当前方法过时了，请使用NewDemoShowMe来替换它",true)]
    public static void OldDemoShowMe()
    {
        Debug.Log("OldDemoShowMe");
    }

    public static void NewDemoShowMe()
    {
        Debug.Log("NewDemoShowMe");
    }
}


public class UseObsoleteDemo : MonoBehaviour {
    private void Start()
    {
        // DemoFrame.OldDemoShowMe();
        DemoFrame.NewDemoShowMe();
    }
}