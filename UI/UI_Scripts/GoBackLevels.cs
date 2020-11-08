using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GoBackLevels : MonoBehaviour {
    private void Start()
    {
        GameManager.GetInstance().currentStars = 3;
    }


    public void OnBackButtonClick()
    {
        
        //记录当前关卡所得的星星数量(引用类型存储数据的引用）
        List<int> data = GameManager.GetInstance().levelsData;
        //Debug.Log(GameManager.GetInstance().levelsData.Count);

        //这关之前没有进入过
        if (GameManager.GetInstance().levelIndex >= data.Count)
        {
            //添加数据
            data.Add(GameManager.GetInstance().currentStars);
        }
        else
        {
            //如果之前的战绩比这次要低
            if (data[GameManager.GetInstance().levelIndex] 
                < GameManager.GetInstance().currentStars)
            {
                //重新设置该关卡的星星数量
                data[GameManager.GetInstance().levelIndex] 
                    = GameManager.GetInstance().currentStars;
            }
        }

        //切换场景        
        SceneManager.LoadScene("Levels");
    }
}