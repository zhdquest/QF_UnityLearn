using System;
using UnityEngine;
using System.Collections;

public class FrameTest : MonoBehaviour {
    private void Start()
    {
        SQLFramework.GetInstance().OpenDatabase("HeroDatabase");

        object result = SQLFramework.GetInstance().SelectSingleData(
            "Select HeroAD From HeroTable Where HeroName='EZ'");
        
        Debug.Log(result);
    }

    private void OnApplicationQuit()
    {
        SQLFramework.GetInstance().CloseDatabase();
    }
}