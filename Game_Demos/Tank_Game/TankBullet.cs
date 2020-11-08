using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class TankBullet : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 3f;
    //炮弹飞行的方向向量
    public Vector3 dir;

    private void Start()
    {
        //延时销毁
        Destroy(this.gameObject,3f);
    }

    void Update()
    {
        transform.position += dir.normalized * Time.deltaTime * moveSpeed;
    }
}
