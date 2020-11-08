using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_Tank : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 3f;
    [Header("转速速度")]
    public float turnSpeed = 3f;
    [Header("发射炮弹的时间间隔")]
    public float fireInterval = 3f;
    [Header("炮弹预设体")]
    public GameObject bulletPrefab;
    [Header("炮弹生成点偏移量")]
    public float firePointOffset = 1.4f;
    [Header("炮弹移动速度")]
    public float bulletMoveSpeed = 3f;

    private Transform tankGun;
    private float timer;
    private float hor, ver;
    private bool fire;

    private void Awake()
    {
        tankGun = transform.Find("Buttom/Top/Gun");
    }

    void Update()
    {
        //TEST:
        if (Input.GetKeyDown(KeyCode.P))
        {
            EnemyTankController.CanMove = !EnemyTankController.CanMove;
        }

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        fire = Input.GetButtonDown("Fire");
        
        transform.position += ver * transform.forward * Time.deltaTime * moveSpeed;
        transform.eulerAngles += hor * Vector3.up * turnSpeed;

        if (fire)
        {
            //炮弹生成坐标
            Vector3 pos = tankGun.position + tankGun.up * firePointOffset;
            //生成
            GameObject crtBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
            //向前移动
            crtBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletMoveSpeed;
            //定时销毁
            Destroy(crtBullet,3f);
        }
    }
}
