using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTimeAndMathf : MonoBehaviour
{

    //时间缩放
    public float timeScale = 1f;

    public float moveSpeed = 2;

    private void FixedUpdate()
    {
        Debug.Log(Time.fixedDeltaTime);
        transform.position += Vector3.forward * Time.fixedDeltaTime * moveSpeed;
    }

    void Update()
    {
        // Debug.Log(Time.time);//游戏过来多长时间
        Debug.Log(Time.deltaTime);//每帧的时间间隔

        //调整时间缩放
        Time.timeScale = timeScale;

        // transform.position += Vector3.forward * Time.deltaTime * moveSpeed;
        // transform.position = Vector3.Lerp(transform.position,Vector3.forward * 5 ,Time.deltaTime * moveSpeed);



    }
}
