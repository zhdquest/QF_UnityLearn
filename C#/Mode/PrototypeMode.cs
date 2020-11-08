using System;
using UnityEngine;
using System.Collections;

namespace PrototypeModeSpace
{

    public interface NeedCopy
    {
        //浅拷贝
        NeedCopy ShallowClone();
        //深拷贝
        NeedCopy DeepClone();
    }

    public class ExtentEffect : NeedCopy
    {
        public string msg;

        public ExtentEffect(string msg)
        {
            this.msg = msg;
        }

        public NeedCopy ShallowClone()
        {
            return this.MemberwiseClone() as NeedCopy;
        }

        public NeedCopy DeepClone()
        {
            //拷贝
            ExtentEffect extentEffect = this.MemberwiseClone() as ExtentEffect;
            //将信息拷贝
            extentEffect.msg = String.Copy(msg);
            //将对象返回
            return extentEffect;
        }
    }

    public class AttackEquip : NeedCopy
    {
        public AttackEquip(string equipName, ExtentEffect _extentEffect)
        {
            this.equipName = equipName;
            this._extentEffect = _extentEffect;
        }

        public string equipName;
        public ExtentEffect _extentEffect;

        public NeedCopy ShallowClone()
        {
            return this.MemberwiseClone() as NeedCopy;
        }

        public NeedCopy DeepClone()
        {
            //拷贝
            AttackEquip attackEquip = this.MemberwiseClone() as AttackEquip;
            //名称拷贝
            attackEquip.equipName = string.Copy(equipName);
            //额外特效拷贝
            attackEquip._extentEffect = _extentEffect.DeepClone() as ExtentEffect;
            //返回
            return attackEquip;
        }
    }


    public class PrototypeMode : MonoBehaviour {
        private void Start()
        {
            AttackEquip wj = new AttackEquip("无尽之刃",new ExtentEffect("暴击率提高..."));
            
            //进行深层次拷贝
            AttackEquip newWJ = wj.ShallowClone() as AttackEquip;

            newWJ.equipName = "无尽大剑";
            newWJ._extentEffect.msg = "秒人分分钟...";
            
            Debug.Log(wj.equipName);
            Debug.Log(wj._extentEffect.msg);
        }
    }
}


