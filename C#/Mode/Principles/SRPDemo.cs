using UnityEngine;

#region Null-SRP

public enum HeroType
{
    CloseCombat,//近战
    LongDistance//远程
}

//public class Hero
//{
//    private HeroType _heroType;
    
//    public void Attack()
//    {
//        if (_heroType == HeroType.CloseCombat)
//        {
//            Debug.Log("近距离攻击，挥砍..");
//        }
//        else if(_heroType == HeroType.LongDistance)
//        {
//            Debug.Log("远距离攻击，射击..");
//        }
//    }
//}

#endregion

#region SPR

/// <summary>
/// 英雄基类（...）
/// </summary>
public class HeroBase
{
    public virtual void Attack()
    {
        Debug.Log("英雄正在攻击...");
    }
}

public class CloseCombatHero : HeroBase
{
    public override void Attack()
    {
        Debug.Log("近战正在攻击...");
    }
}

public class LongDistanceHero : HeroBase
{
    public override void Attack()
    {
        Debug.Log("远程英雄正在攻击...");
    }
}


#endregion

public class SRPDemo : MonoBehaviour {
    
    
    
    
}