using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Vector : MonoBehaviour
{
    Vector3 vec1;
    Vector3 vec2;
    void _Calculate()
    {
        //向量方向保持不变，长度为1，返回一个新的归一化向量。
        Vector3 no = vec1.normalized;
        //向量方向保持不变，长度为1，改变的是当前向量
        Vector3 no1 = Vector3.Normalize(vec1);

        //返回该向量的长度
        float m = vec1.magnitude;
        //返回该向量的平方长度；只将大小用于比较距离的目的，则用此方法节省计算
        float sm = vec1.sqrMagnitude;

        //返回两向量之间的距离
        float d = Vector3.Distance(vec1, vec2);
        //返回两向量之间的角度
        float a = Vector3.Angle(vec1, vec2);

        //返回两向量的点积的值（垂直时值为0）
        float dot = Vector3.Dot(vec1, vec2);
        //返回两向量的叉积的值（左手规则，有先后顺序）
        Vector3 c= Vector3.Cross(vec1, vec2);

        //在两个向量之间进行线性插值。
        //第三个参数表示两个端点之间距离的百分比
        //可用于实现变速效果（不断改变起点的位置）
        Vector3 l = Vector3.Lerp(vec1, vec2, 0.5f);
    }

    void _Directon()
    {
        //(0,0,1)
        Vector3 f = Vector3.forward;
        //(0,1,0)
        Vector3 u = Vector3.up;
        //(1,0,0)
        Vector3 r = Vector3.right;
        //(0,0,0)
        Vector3 z = Vector3.zero;
        //(1,1,1)
        Vector3 o = Vector3.one;
    }

}


