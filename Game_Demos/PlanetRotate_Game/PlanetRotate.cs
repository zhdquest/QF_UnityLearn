
using UnityEngine;
using System.Collections;
/*
实现八大行星与太阳的自转公转效果(如效果图)
要求（必做）：
1、不同行星的大小和位置不同
2、不同行星的自转速度和公转速度不同
3、不同行星公转所环绕的轴不同
4、不同行星的颜色不同
要求（选做）：
1、不同行星的材质不同
2、保证行星公转面包含太阳 **
3、设置行星公转拖尾效果
*/

public class PlanetRotate : MonoBehaviour
{

    public Vector3 pos;
    public float scale;
    /// <summary>
    /// 自转速度
    /// </summary>
    public float rotateSelfSpeed = 3;
    /// <summary>
    /// 公转速度
    /// </summary>
    public float rotateAroundSpeed = 3;
    /// <summary>
    /// 太阳
    /// </summary>
    public Transform sun;
    /// <summary>
    /// 公转轴
    /// </summary>
    public Vector3 axis = new Vector3(0, 1, 0);

    public bool hasRotateAround = true;

    void Start()
    {
        //如何设置位置
        transform.position = pos;
        //如何设置缩放
        transform.localScale = scale * Vector3.one;

        if (hasRotateAround)
        {
            //CountStarNewPos();
            SimplerCountStarNewPos();
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 1, 0) * rotateSelfSpeed);
        if (hasRotateAround)
        {
            transform.RotateAround(sun.position, axis, rotateAroundSpeed);
        }
    }

    //通过旋转移动位置
    private void SimplerCountStarNewPos()
    {
        //计算太阳指向行星的方向向量
        Vector3 starSun = transform.position - sun.position;
        //求公转轴与方向向量的夹角
        float angle = Vector3.Angle(starSun, axis);

        if (angle > 90)
        {
            angle = angle - 90;
        }
        else
        {
            angle = 90 - angle;
        }

        Vector3 normalAxis = Vector3.Cross(axis, starSun);

        transform.RotateAround(sun.position, normalAxis, angle);
    }

    //通过计算投影坐标移动位置
    private void CountStarNewPos()
    {
        //计算太阳指向行星的方向向量
        Vector3 starSun = transform.position - sun.position;
        //计算太阳到行星的距离
        float starSunDistance = starSun.magnitude;
        //求公转轴与方向向量的夹角
        float angle = Vector3.Angle(starSun, axis);
        //投影坐标
        Vector3 projectPoint = Vector3.zero;
        //判断
        if (angle > 90)
        {
            angle = 180 - angle;
            float axisDis = starSunDistance * Mathf.Cos(angle);
            projectPoint = (-axis.normalized * axisDis + sun.position);
        }
        else
        {
            float axisDis = starSunDistance * Mathf.Cos(angle);
            projectPoint = (axis.normalized * axisDis + sun.position);
        }

        GameObject m = GameObject.CreatePrimitive(PrimitiveType.Cube);
        m.transform.position = projectPoint;
        //求出新的方向向量
        Vector3 newDir = transform.position - projectPoint;
        //求出行星新的位置
        transform.position = newDir + sun.position;
    }
}