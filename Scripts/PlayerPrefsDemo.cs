using System;
using UnityEngine;
using System.Collections;

public class PlayerPrefsDemo : MonoBehaviour {
    
    private void Start()
    {
        #region Float存储

        //存储到本地
        // PlayerPrefs.SetFloat("Score",99.9f);
        // //打印
        // Debug.Log("存储成功！");
        
        

        #endregion

        #region 存储String

        PlayerPrefs.SetString("Score","100分");
        Debug.Log("存储成功！");
        
        #endregion

        #region 读取Float

        //读取float
        // float value = PlayerPrefs.GetFloat("Score");
        //
        // Debug.Log("Value: " + value);

        #endregion
        
    }
}