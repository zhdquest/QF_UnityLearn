using System;
using UnityEngine;

namespace StrategyMode
{

    public class Hero
    {
        private string name;
        public Transform heroTra;
        public float hp = 100;

        public Hero(string name,Transform heroTra)
        {
            this.name = name;
            this.heroTra = heroTra;
        }

        public void Attack(AttackStrategy attackStrategy,Hero targetHero)
        {
            float damage = attackStrategy.TakeDamage(targetHero);
            targetHero.TakeDamage(damage);
        }

        public void TakeDamage(float damage)
        {
            hp -= damage;
            
            //....
        }
    }

    /// <summary>
    /// 抽象攻击策略
    /// </summary>
    public abstract class AttackStrategy
    {
        public abstract float TakeDamage(Hero hero);
    }
    
    /// <summary>
    /// 圆形攻击
    /// </summary>
    public class CircleAttack : AttackStrategy
    {
        //技能中心
        private Vector3 skillCenter;
        //技能半径
        private float skillRadius;
        //中心伤害
        private float centerDamage;
        //边缘伤害
        private float edgeDamage;

        public CircleAttack(Vector3 skillCenter,float skillRadius,
            float centerDamage,float edgeDamage)
        {
            this.skillCenter = skillCenter;
            this.skillRadius = skillRadius;
            this.centerDamage = centerDamage;
            this.edgeDamage = edgeDamage;
        }

        public override float TakeDamage(Hero hero)
        {
            //计算玩家与技能中心点的距离
            float dis = Vector3.Distance(hero.heroTra.position, skillCenter);

            if (dis <= skillRadius)
            {
                return edgeDamage + (skillRadius - dis) / skillRadius * (centerDamage - edgeDamage);
            }
            else
            {
                return 0;
            }
        }
    }

    public class RangleAttack : AttackStrategy
    {
        //技能中心
        private Vector3 skillCenter;
        //宽度
        private float width;
        //高度
        private float height;
        //伤害值
        private float damage;
        //释放技能的英雄
        private Hero releaseHero;

        public RangleAttack(Vector3 skillCenter,
            float width,float height,float damage,
            Hero releaseHero)
        {
            this.skillCenter = skillCenter;
            this.width = width;
            this.height = height;
            this.damage = damage;
            this.releaseHero = releaseHero;
        }

        public override float TakeDamage(Hero hero)
        {
            Vector3 dir = hero.heroTra.position- skillCenter;

            //方向向量在释放者水平方向的投影向量
            Vector3 horDir = Vector3.Project(dir, releaseHero.heroTra.right);
            //方向向量在释放者垂直方向的投影向量
            Vector3 verDir = Vector3.Project(dir, releaseHero.heroTra.forward);

            if (horDir.magnitude <= width &&
                verDir.magnitude <= height)
            {
                return damage;
            }

            return 0;
        }
    }

    public class FanshapedAttack : AttackStrategy
    {
        //技能中心
        private Vector3 skillCenter;
        //伤害值
        private float damage;
        //释放技能的英雄
        private Hero releaseHero;
        //扇形技能的夹角
        private float angle;
        //扇形半径
        private float skillRadius;

        public FanshapedAttack(Vector3 skillCenter,
            float angle,float damage,
            Hero releaseHero)
        {
            this.angle = angle;
            this.skillCenter = skillCenter;
            this.damage = damage;
            this.releaseHero = releaseHero;
        }
        
        public override float TakeDamage(Hero hero)
        {
            //距离
            float dis = Vector3.Distance(hero.heroTra.position, releaseHero.heroTra.position);
            //夹角
            float angle = Vector3.Angle(releaseHero.heroTra.forward, hero.heroTra.position - releaseHero.heroTra.position);
            //距离超出半径
            if (dis > skillRadius)
                return 0;
            //不在扇形夹角范围内
            if (angle > this.angle / 2)
                return 0;
            return damage;
        }
    }

    public class StrategyDemo : MonoBehaviour
    {
        //释放技能的英雄
        public Transform releaseHero;
        //受伤的英雄
        public Transform hurtHero;
        
        private void Start()
        {
            Hero akali = new Hero("阿卡丽",releaseHero);
            Hero fatiao = new Hero("发条魔灵",hurtHero);
            
            CircleAttack circleAttack = new CircleAttack(Vector3.zero,
                10,100,5);
            
            RangleAttack rangleAttack = new RangleAttack(
                Vector3.zero,10,5,50,fatiao);
            
            //圆形攻击
            akali.Attack(circleAttack,fatiao);
            //矩形攻击
            akali.Attack(rangleAttack,fatiao);
        }
    }

}


