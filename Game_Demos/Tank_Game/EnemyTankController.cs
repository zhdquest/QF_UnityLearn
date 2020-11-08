using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTankController : MonoBehaviour
{
    public static bool CanMove = true;
    
    [Header("移动速度")]
    public float moveSpeed = 3f;
    [Header("转身速度")]
    public float turnSpeed = 3f;
    [Header("炮弹的预设体")]
    public GameObject bulletPrefab;
    [Header("炮弹发射的时间间隔")]
    public float fireInterval = 3f;
    [Header("随机旋转的时间间隔")]
    public float randomRotateInterval;
    [Header("炮弹的移动速度")]
    public float bulletMoveSpeed = 3f;
    [Header("炮弹发射点的偏移量")]
    public float firePointOffset = 1.4f;
    
    //玩家
    private Transform player;
    //炮管的Transform
    private Transform tankGun;
    //坦克指向玩家的方向向量
    private Vector3 dir;
    //计时器
    private float timer;
    //射线碰撞检测器
    private RaycastHit hit;
    //发射点
    private Vector3 firePoint;
    //随机的y的角度
    private int y = 0;
    //随机的四元数
    private Quaternion randomQua;
    
    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        tankGun = transform.Find("Buttom/Top/Gun");
    }

    void Update()
    {
        if(!CanMove)
            return;
        //计算当前坦克与玩家的距离
        float dis = Vector3.Distance(transform.position, player.position);
        //坦克指向玩家的方向向量
        dir = player.position - transform.position;
        //计算位置
        firePoint = tankGun.position + tankGun.up * firePointOffset;
        
        if (dis < 10)
        {
            Fire();
        }
        else if(dis < 20)
        {
            MoveToPlayer();
        }
        else
        {
            RandomMove();
        }
    }

    /// <summary>
    /// 转向玩家
    /// </summary>
    private void TurnToPlayer()
    {
        //将方向向量转换为四元数
        Quaternion targetQua = Quaternion.LookRotation(dir);
        //Lerp过去
        transform.rotation = Quaternion.Lerp(transform.rotation,targetQua,Time.deltaTime * turnSpeed);
    }

    /// <summary>
    /// 当前坦克是否已经看向玩家
    /// </summary>
    /// <returns></returns>
    private bool HasLookAtPlayer()
    {
        return Vector3.Angle(dir, transform.forward) < 1;
    }

    /// <summary>
    /// 向玩家开炮
    /// </summary>
    private void Fire()
    {
        //转向玩家
        TurnToPlayer();
        //判断是否已经瞄准完成
        if (HasLookAtPlayer())
        {
            //计时器计时
            timer += Time.deltaTime;
            if (timer > fireInterval)
            {
                //从炮口位置向玩家发射物理射线
                if (Physics.Raycast(firePoint, dir,out hit,10))
                {
                    //查看碰撞体的根对象是否是玩家
                    if (hit.transform.root.tag == "Player")
                    {
                        //生成炮弹
                        GameObject crtBullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);
                        //给速度
                        crtBullet.GetComponent<Rigidbody>().velocity = transform.forward * bulletMoveSpeed;
                        //定时销毁
                        Destroy(crtBullet,3f);
                    }
                }
                //计时器归零
                timer = 0;
            }
        }
    }

    private void MoveToPlayer()
    {
        //转向玩家
        TurnToPlayer();
        //检测炮管前方两米的正方形范围内是否有友军
        if (Physics.CheckBox(firePoint + transform.forward,
            new Vector3(1, 1, 1),
            Quaternion.identity, ~(1 << 9)))
        {
            //直接返回
            return;
        }

        //向前移动
        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }

    private void RandomMove()
    {
        timer += Time.deltaTime;
        
        if (timer > randomRotateInterval)
        {
            y = Random.Range(0, 360);
            randomQua = Quaternion.Euler(Vector3.up * y);
            timer = 0;
        }
        //lerp过去
        transform.rotation = Quaternion.Lerp(transform.rotation,
            randomQua,Time.deltaTime * turnSpeed);

        if (Physics.Raycast(firePoint, transform.forward, out hit, 3))
        {
            y = Random.Range(0, 360);
            randomQua = Quaternion.Euler(Vector3.up * y);
            return;
        }

        transform.position += transform.forward * Time.deltaTime * moveSpeed;
    }
}