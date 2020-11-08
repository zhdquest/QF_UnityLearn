using System;
using UnityEngine;
using Utilty;

namespace fac
{
    public class CountFactory
    {
        public static int GetResult(int num01, int num02, string sign)
        {
            switch (sign)
            {
                case "+":
                    return num01 + num02;
                case "-":
                    return num01 - num02;
                case "*":
                    return num01 * num02;
                default:
                    return 0;
            }
        }
    }

    public class MyHero
    {
        public float magic = 100;
        public float[] cds;
        public string name;

        public MyHero(string name)
        {
            this.name = name;
            cds = new float[] { 0, 0 };
        }

    }

    public abstract class Skill
    {
        //技能操作符
        private string sign = "Q";
        public int skillIndex = 0;

        private float cd;
        protected float magicNeed;

        public abstract void SkillRelease();
    }

    public class WWWW : Skill
    {
        //技能释放的坐标
        public Vector3 pos;

        public override void SkillRelease()
        {
            skillIndex = 1;
            if (pos != null)
            {
                Debug.Log("释放了WWW技能...");
            }
        }
    }

    public class QQQ : Skill
    {
        public MyHero targetHero;

        public override void SkillRelease()
        {
            skillIndex = 0;
            //如果蓝量够，且cd转好了
            if (targetHero.magic >= this.magicNeed
                && targetHero.cds[skillIndex] == 0
                && targetHero != null)
            {
                Debug.Log("使用当前QQQ的技能");
            }
        }
    }

    public class SkillOperationFactory
    {
        public static Skill GetSkillOperation(string sign)
        {
            if (sign == "Q")
            {
                return new QQQ();
            }
            else if (sign == "R")
            {
                return new RRR();
            }
            else
            {
                return new WWWW();
            }
        }
    }

    public class RRR : Skill
    {
        public override void SkillRelease()
        {

        }
    }

    public class SimplerFactoryDemo : MonoBehaviour
    {

        private void Start()
        {
            Skill skill = SkillOperationFactory.GetSkillOperation("Q");
            QQQ q = skill as QQQ;
            q.targetHero = new MyHero("阿卡丽");
            q.SkillRelease();
            skill = SkillOperationFactory.GetSkillOperation("W");
            WWWW w = skill as WWWW;
            w.pos = new Vector3(1, 1, 1);
            skill.SkillRelease();
            Skill rSkill = SkillOperationFactory.GetSkillOperation("R");

            GameObject heroPrefab = Resources.Load<GameObject>("heroPrefab");

            //Other Script
            GameObject otherHeroPrefab = Resources.Load<GameObject>("heroPrefab");


            //Test:资源动态加载
            // PrefabManager.GetInstance().CreateGameObjectByPrefab("Body");

            // PrefabManager.GetInstance().CreateGameObjectByPrefab(
            //     "Body", new Vector3(1, 1, 1), Quaternion.identity);

            PrefabManager.GetInstance().CreateGameObjectByPrefab(
                "Body", Camera.main.transform, Vector3.zero, Quaternion.identity);
        }
    }
}