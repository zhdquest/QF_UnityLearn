using System;
using UnityEngine;
using System.Collections.Generic;

public class FPSGunManager : MonoBehaviour
{
    //管理的枪支
    private List<FPSGun> managerdGuns;
    
    //当前使用的枪支
    private FPSGun currentGun;

    //枪支编号
    private int index = 0;

    private void Awake()
    {
        managerdGuns = new List<FPSGun>();
    }

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            //添加每一把抢
            managerdGuns.Add(transform.GetChild(i).GetComponent<FPSGun>());
        }

        if (managerdGuns.Count > 0)
        {
            //当前先使用的是第一把抢
            currentGun = managerdGuns[index];
        }
    }

    private void Update()
    {
        //当前是否有一把枪
        if(!currentGun)
            return;

        if (Input.GetButtonDown("Fire"))
        {
            //让当前使用的枪开枪
            currentGun.Fire();
        }

        if (Input.GetButtonDown("Reload"))
        {
            //让当前使用的枪换弹
            currentGun.Reload();
        }
        
        //换枪
        if(Input.GetButtonDown("Switch"))
        {
            //当前枪支设置为非激活
            currentGun.gameObject.SetActive(false);
            //方法一：
            // if (++index < managerdGuns.Count)
            // {}
            // else
            // { index = 0; }
            //方法二：
            //计算新枪的换号
            index = ++index % managerdGuns.Count;
            //更新当前枪支
            currentGun = managerdGuns[index];
            //新枪
            currentGun.gameObject.SetActive(true);
        }
    }
}