using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Physics : MonoBehaviour
{
    Rigidbody rib;
    void _Property()
    {
        //刚体的速度矢量
        rib.velocity = new Vector3(1, 1, 1);
        //刚体的角速度矢量
        rib.angularVelocity = new Vector3(1, 1, 1);
        //受到的阻力
        rib.drag = 10;
        //受到的角阻力
        rib.angularDrag = 10;

        //控制重力是否影响该刚体
        rib.useGravity = false;
        //若为true，则物理不影响刚体
        rib.isKinematic = true;
        //控制物理是否会改变对象的旋转
        rib.freezeRotation = true;
    }

    void _Method()
    {
        //给刚体添加力，可设置添加力的模式
        rib.AddForce(Vector3.up);
        //给刚体添加爆炸效果的力
        rib.AddExplosionForce(1f, Vector3.zero,1f);
    }

    void _Ray()
    {
        
    }
}
