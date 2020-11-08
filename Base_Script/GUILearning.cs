using System;
using UnityEngine;

public class GUILearning : MonoBehaviour
{
    private string textFieldText = "今天风好大...";

    private float sliderValue = 50;
    
    private void OnGUILearning()
    {
        //手动布局【GUI】
        bool isClick = GUI.Button(new Rect(100, 100, 200, 50), "Login");
        if (isClick)
        {
            Debug.Log("登录被点击了........");
        }
        if (GUI.Button(new Rect(300, 100, 200, 50), "Register"))
        {
            Debug.Log("注册被点击了........");
        }
        textFieldText = GUI.TextField(new Rect(100, 300, 200, 50),textFieldText);
        sliderValue = GUI.HorizontalSlider(new Rect(100, 500, 200, 50),sliderValue,0,100);
        
        //自动布局【GUILayout】
        GUILayout.Button("注册赢大奖");
        
        textFieldText = GUILayout.TextField(textFieldText);
    }
}