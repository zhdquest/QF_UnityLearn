using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadQuaternion : MonoBehaviour
{
    
    public float turnSpeed = 3f;
    public Transform target;
    
    void Start()
    {
        //空旋转【相当于欧拉角的（0，0，0）】
        Debug.Log(Quaternion.identity);
        //将当前角色的旋转设置空旋转
        transform.rotation = Quaternion.identity;
    }

    void Update()
    {
        //transform.LookAt(target);
        //插值旋转
        // transform.rotation 起点四元数

        //玩家指向敌人的方向向量
        Vector3 dir = target.position - transform.position;
        //目标四元数
        Quaternion targetQua = Quaternion.LookRotation(dir);
        
        // transform.position = Vector3.Lerp(transform.position,Vector3.right*5,0.02f);

        //插值转身
        transform.rotation = Quaternion.Lerp(transform.rotation, targetQua, 0.02f * turnSpeed);
    }
}
