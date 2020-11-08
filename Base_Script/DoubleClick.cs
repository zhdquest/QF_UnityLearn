using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DoubleClick : MonoBehaviour
{
    [Header("双击的时间间隔限定")]
    public float doubleClickInterval = 0.5f;

    //是否有过一次点击
    private bool hasOneClick = false;
    //计时器
    private float timer = 0;
    //炮管网格渲染器
    private MeshRenderer gunMeshRenderer;

    private void Awake()
    {
        gunMeshRenderer = transform.Find("Buttom/Top/Gun").
            GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (hasOneClick)
        {
            timer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            //当前是第一次点击
            if (!hasOneClick)
            {
                //标记完成了一次点击
                hasOneClick = true;
            }
            //当前是第二次点击
            else
            {
                //第二次点击在时间限定范围内
                if (timer < doubleClickInterval)
                {
                    //TODO:完成双击
                    Debug.Log("Double Click");
                    gunMeshRenderer.material.color = new Color(Random.Range(0f,1),
                        Random.Range(0f,1),
                        Random.Range(0f,1));
                    //把标志位重置
                    hasOneClick = false;
                }
                //计时器归零
                timer = 0;
            }
        }
    }
}
