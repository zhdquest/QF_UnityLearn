using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Protate : MonoBehaviour
{   [Header("旋转中心")]
    public Transform PointPlanet;
    [Header("自转速度")]
    public float SelfRotateSpeed;
    [Header("公转速度")]
    public float RotateSpeed;
    //public float distance;

    private Vector3 RotatePlane;

    // Start is called before the first frame update
    void Start()
    {
        //transform.position = new Vector3(PointPlanet.position.x + distance, PointPlanet.position.y + distance, PointPlanet.position.z + distance);
        //旋转轴平面，由哪两个向量的叉积确定？(只要与AB垂直即可？乘哪个向量好像无关？）
        Vector3 AB = new Vector3(PointPlanet.position.x -this.transform.position.x, PointPlanet.position.y - this.transform.position.y, PointPlanet.position.z - this.transform.position.z);
        RotatePlane = Vector3.Cross(AB,new Vector3(1,0,0));
    }

    // Update is called once per frame
    void Update()
    {
        //自转
        transform.localEulerAngles += new Vector3(0, SelfRotateSpeed, 0);
        //公转（中心、旋转轴、角度）
        transform.RotateAround(PointPlanet.position, RotatePlane,RotateSpeed);
    }
}
