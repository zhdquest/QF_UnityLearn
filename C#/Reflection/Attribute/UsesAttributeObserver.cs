using System;
using System.Reflection;
using UnityEngine;

namespace AAA
{

}

[AddComponentMenu("MY/UsesAttributeObserver")]
//组件依赖
[RequireComponent(typeof(Rigidbody),
    typeof(BoxCollider))]
public class UsesAttributeObserver : MonoBehaviour
{
    [Header("日志延迟时间")]
    [Range(0, 1000)]
    public float logDelayTime = 100;

    [ColorUsage(true)]
    public Color col;

    [SerializeField]//让私有的字段也可以显示在监视器面板
    private string name;

    private void Start()
    {
        InvokeMethodByAttribute(typeof(UsersAttributes), "ShowLog",
            true, true);
    }

    /// <summary>
    /// 执行某个方法依据某个特性
    /// </summary>
    /// <param name="methodClassType">方法所在的类的类型</param>
    /// <param name="methodName">方法名</param>
    /// <param name="isStatic">是否是静态</param>
    /// <param name="isPublic">是否是公有</param>
    /// <param name="methodObj">成员方法所在对象</param> 
    private void InvokeMethodByAttribute(Type methodClassType,
        string methodName, bool isStatic, bool isPublic, object methodObj = null)
    {
        BindingFlags staticFlags = isStatic ? BindingFlags.Static : BindingFlags.Instance;
        BindingFlags pubicFlags = isPublic ? BindingFlags.Public : BindingFlags.NonPublic;
        //获取到该方法
        MethodInfo info = methodClassType.GetMethod(methodName, staticFlags | pubicFlags);

        //获取MyCondition类型的特性
        object[] atts = info.GetCustomAttributes(typeof(MyConditionAttribute), false);

        if (atts.Length > 0)
        {
            info.Invoke(null, null);
        }
        else
        {
            Debug.LogError("没有添加MyCondition特性，该方法不能执行！");
        }
    }

    private void PrintAttributeMsg()
    {
        //获取类型
        Type type = typeof(UsersAttributes);
        //得到该类型的不可继承的特性对象
        object[] atts = type.GetCustomAttributes(false);

        for (int i = 0; i < atts.Length; i++)
        {
            //判断当前特性是不是某个特性类型
            if (atts[i] is AuthorAttribute)
            {
                //将特性转换为该类型
                AuthorAttribute authorObj = atts[i] as AuthorAttribute;

                Debug.Log(authorObj.author);
                Debug.Log(authorObj.LastDate);
            }
        }
    }

}