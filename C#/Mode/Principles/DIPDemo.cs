using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public abstract class ABGun
{
    public abstract void Fire();
}

public class AK47 : ABGun
{
    public override void Fire()
    {
        Debug.Log("AK47棒棒棒...");
    }
}

public class MP5 : ABGun
{
    public override void Fire()
    {
        Debug.Log("MP5嘟嘟嘟...");
    }
}

public class AWK : ABGun
{
    public override void Fire()
    {
        Debug.Log("Duang...");
    }
}

public class FireMan
{
    public string name;
    
    public virtual void FireByGun(ABGun gun)
    {
        Debug.Log(name + "长在开枪...");
        gun.Fire();
    }
}

public partial class Player : FireMan
{
    // public override void FireByGun(ABGun gun)
    // {
    //     Debug.Log("玩家开枪...");
    //     base.FireByGun(gun);
    // }

    // public void PlayerFire(ABGun gun)
    // {
    //     gun.Fire();
    // }
}

public class AI
{
    // public void AIFire(ABGun gun)
    // {
    //     gun.Fire();
    // }
}

public class Monster
{
    
}

public class DIPDemo : MonoBehaviour
{
    private IList<Monster> _monsters;

    public void SetMonster(IList<Monster> monsters)
    {
        _monsters = monsters;
    }

    public void FireByGun(ABGun gun)
    {
        gun.Fire();
    }

    private void Start()
    {
        List<Monster> listMonsters = new List<Monster>();
        SetMonster(listMonsters);

        Monster[] arrayMonsters = new Monster[3];
        SetMonster(arrayMonsters);
        
        FireByGun(new AK47());
        FireByGun(new MP5());
    }
}