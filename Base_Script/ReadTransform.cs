using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadTransform : MonoBehaviour
{
    public Transform lookAtTarget;

    //公共属性以外面监视面板的数值为准
    public float rotateSpeed = 3f;
    public Vector3 rotateAxis;
    
    void Start()
    {
        //Transform组件的两个功能

        #region 1、控制游戏对象的变幻

        //世界坐标
        Debug.Log(transform.position);
        //本地坐标
        Debug.Log(transform.localPosition);
        
        //世界欧拉角
        Debug.Log(transform.eulerAngles);
        //本地欧拉角
        Debug.Log(transform.localEulerAngles);
        
        //世界旋转【四元数】
        Debug.Log(transform.rotation);
        //本地旋转【四元数】
        Debug.Log(transform.localRotation);
        
        //本地缩放
        Debug.Log(transform.localScale);
        
        //方向
        //自身前方的方向向量
        Debug.Log(transform.forward);
        //自身右方的方向向量
        Debug.Log(transform.right);
        //自身上方的方向向量
        Debug.Log(transform.up);
        
        
        #endregion 
        
        #region 2、描述游戏对象的层级关系

        //父对象
        Debug.Log(transform.parent);
        //根对象
        Debug.Log(transform.root);

        //设置父物体
        transform.parent = lookAtTarget;
        //==>
        transform.SetParent(lookAtTarget);
        
        Debug.Log(transform.childCount);

        Debug.Log(transform.GetChild(0));
        
        //遍历所有的子对象
        foreach (Transform tra in transform)
        {
            Debug.Log(tra);
        }

        //遍历所有的子对象
        for (int i = 0; i < transform.childCount; i++)
        {
            Debug.Log(transform.GetChild(i));
        }
        
        //找当前对象的子对象
        Transform sph = transform.Find("Cylinder/Sphere");

        Debug.Log(sph);
        #endregion
    }


    private void Update()
    {
        //角色朝自身前方移动
        // transform.position += transform.forward * 0.05f;
        // transform.Translate(transform.forward * 0.05f);
        
        //自转
        // transform.Rotate(new Vector3(0,1,0),2,Space.World);
        //==>
        // transform.eulerAngles += new Vector3(0,1,0);
        
        // transform.Rotate(new Vector3(0,1,0),2,Space.Self);
        //==>
        // transform.localEulerAngles += new Vector3(0,1,0);
        
        //绕某个点沿某个轴，旋转
        // transform.RotateAround(new Vector3(0, 0, 0), new Vector3(0, 1, 0), 5);

        // transform.LookAt(new Vector3(0,0,0));
        
        transform.LookAt(lookAtTarget);
    }
}
