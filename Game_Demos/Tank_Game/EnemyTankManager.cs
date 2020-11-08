using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyTankManager : MonoBehaviour
{
    [Header("敌方坦克预设体")]
    public GameObject enemyTankPrefab;
    [Header("敌方坦克生成间隔")]
    public float createInterval = 3f;
    [Header("坦克的最大数量")]
    public int enemyTankMax = 30;
    [Header("生成坦克的开关")]
    public bool IsCreate = true;
    
    //计时器
    private float timer = 0;
    //计数器
    private static int CurrentEnemyTank = 0;

    private void Update()
    {
        if (IsCreate)
        {
            InitEnemyTank();
        }
    }

    private void InitEnemyTank()
    {
        //如果坦克数量已经超出限制，则不再生成坦克
        if(CurrentEnemyTank >= enemyTankMax)
            return;
        timer += Time.deltaTime;
        if (timer > createInterval)
        {
            //TODO:生成坦克
            //随机位置
            Vector3 pos = RandomTankPos();
            //随机旋转度数
            int y = Random.Range(0, 360);
            //生成坦克
            GameObject crtTank = Instantiate(enemyTankPrefab,
                pos,
                Quaternion.Euler(Vector3.up * y));
            timer = 0;

            CurrentEnemyTank++;
        }
    }


    /// <summary>
    /// 随机一个坦克的坐标
    /// </summary>
    /// <returns>返回随机坐标</returns>
    private Vector3 RandomTankPos()
    {
        float x = 0, z = 0;

        do
        {
            //随机坐标
            x = Random.Range(-20f,20f);
            z = Random.Range(-20f,20f);
        } while (!CheckPosCanUse(new Vector3(x, 0, z)));

        return new Vector3(x,0,z);
    }

    /// <summary>
    /// 检测坐标是否可用
    /// </summary>
    /// <param name="pos">被检测的坐标</param>
    /// <returns></returns>
    private bool CheckPosCanUse(Vector3 pos)
    {
        //如果检测到了其他碰撞体，则该点不可用，否则可用
        return !Physics.CheckBox(pos,
            new Vector3(2,1,2),
            Quaternion.identity, ~(1 << 9));
    }
}
