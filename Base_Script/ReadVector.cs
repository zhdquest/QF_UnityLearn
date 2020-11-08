using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadVector : MonoBehaviour
{
    void Start()
    {
        //创建一个三维向量
        Vector3 dir = new Vector3(1, 2, 3);
        //创建一个二维向量
        Vector2 dir2 = new Vector2(3, 3);
        //创建一个四维向量
        Vector4 dir4 = new Vector4(1, 2, 3, 4);

        dir.x = 4;

        //获取一个向量的单位向量
        Vector3 normalDir = dir.normalized;
        //将当前向量变成单位向量
        dir.Normalize();
        //向量的长度【模】
        float mag = dir.magnitude;
        //模的平方【用来做向量长度的对比】
        float sqrMag = dir.sqrMagnitude;

        #region Vector3 Static

        // Vector3.forward
        // Vector3.back
        // Vector3.left
        // Vector3.right
        // Vector3.up
        // Vector3.down
        // Vector3.zero
        // Vector3.one

        Vector3 pointA = Vector3.forward;
        Vector3 pointB = Vector3.right;

        //求两个坐标的距离
        float dis = Vector3.Distance(pointA, pointB);

        Vector3 dirA = Vector3.one;
        Vector3 dirB = Vector3.right;

        //求两个向量的夹角
        float angle = Vector3.Angle(dirB, dirA);

        float newAngle = Vector3.Angle(new Vector3(1, 1, 0), Vector3.zero);

        //求两个向量的点乘
        float dot = Vector3.Dot(dirA, dirB);

        //求两个向量的叉乘【求两个向量的法向量】
        Vector3 normal = Vector3.Cross(dirA, dirB);

        //求一个向量在某个方向上的投影向量
        Vector3 pro = Vector3.Project(new Vector3(1, 1, 0), Vector3.right);

        //求两个向量中间的插值坐标
        Vector3.Lerp(new Vector3(1, 0, 0), new Vector3(5, 0, 0), 0.5f);

        #endregion

    }

    private void Update()
    {
        // transform.position += Vector3.down * 0.02f;

        transform.position = Vector3.Lerp(transform.position, new Vector3(5, 0, 0), 0.03f);
    }
}