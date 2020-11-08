using System;
using System.Reflection;
using UnityEngine;
using Hero = Framework.Hero;

namespace Framework
{

    public class Role
    {
        
    }

    public class Hero : Role
    {
        private static int HeroCount = 180;
        
        private string name = "xiaoming";
        public int attack = 100;

        public string GetName()
        {
            return name;
        }

        private void ShowMe(string sign)
        {
            Debug.Log("Infomation:" + name + attack + "---" + sign);
        }

        private void ShowMe()
        {
            Debug.Log("Infomation:" + name + attack);
        }

        public Hero(int days)
        {
            Debug.Log("通过一个整型参数的构造实例化了对象");
            this.name = "特殊标记英雄";
            this.attack = days;
        }

        private Hero(string sign)
        {
            Debug.Log("通过私有一个字符串参数的构造实例化了对象");
            this.name = "隐藏英雄";
            this.attack = 10000;
        }

        private Hero()
        {
            Debug.Log("通过私有无参构造函数实例完毕！");
        }

        public Hero(string name, int attack)
        {
            this.name = name;
            this.attack = attack;
        }
    }
}

//enum EquipType
//{
    
////}

//class Person
//{
//    public string name = "xiaoming";
//    public char sex = 'M';
//}

public class RefOperation : MonoBehaviour
{
    private Framework.Hero dmxy;

    private void Start()
    {
        ShowTypeMethod();
    }

    /// <summary>
    /// 如何获取类型
    /// </summary>
    private void RefGetType()
    {
        //①通过typeof获取某个类的类型
        System.Type personType = typeof(Person);
        System.Type heroType = typeof(Framework.Hero);
        //②通过一个对象获取该对象所对应的类的类型
        System.Type dmxyType = dmxy.GetType();
        //③通过类的名称字符串获取对应的类型
        System.Type strType = System.Type.GetType("Person");
        System.Type newStrType = System.Type.GetType("Framework.Hero");
    }

    /// <summary>
    /// 看看type里面有哪些字段
    /// </summary>
    private void ShowTypeField()
    {
        //获取类型
        Type heroType = typeof(Framework.Hero);
        //查看类型的名字
        Debug.Log("Name:" + heroType.Name);
        //查看类型的全名
        Debug.Log("FullName:" + heroType.FullName);
        //查看程序集名称
        Debug.Log("Assembly:" + heroType.Assembly);
        //加上程序集的全名
        Debug.Log("Ass-Name:" +heroType.AssemblyQualifiedName);
        //获取该类型的父类
        Debug.Log("BaseType:" + heroType.BaseType.BaseType);

        Type equipTypeType = typeof(EquipType);
    }

    /// <summary>
    /// 看看Type里的方法
    /// </summary>
    private void ShowTypeMethod()
    {
        //获取类型
        Type heroType = typeof(Framework.Hero);
        //获取一个类的某个成员
        // MemberInfo[] infos = heroType.GetMember("name");
        //打印结果
        // Debug.Log(infos[0].MemberType);

        Type goType = typeof(GameObject);
        
        //获取一个类的所有非公有静态成员
        MemberInfo[] memberInfos = goType.GetMembers(BindingFlags.NonPublic | BindingFlags.Static);

        for (int i = 0; i < memberInfos.Length; i++)
        {
            Debug.Log(memberInfos[i].Name + "|" + memberInfos[i].MemberType);
        }

        #region 通过反射获取某个对象的私有成员字段
        //声明一个该类的对象
        Framework.Hero akl = new Framework.Hero("阿卡丽",123);
        //获取该类型的字段信息【name】
        FieldInfo fieldInfo = heroType.GetField("name",BindingFlags.NonPublic | BindingFlags.Instance);
        //获取该对象，该字段的值
        string aklName = fieldInfo.GetValue(akl).ToString();
        //设置该对象，该字段的值
        fieldInfo.SetValue(akl,"离群之刺");
        Debug.Log("aklName:" + akl.GetName());
        #endregion

        #region 通过反射获取某个类的私有静态字段

        //获取该类型的字段信息【HeroCount】
        FieldInfo newFieldInfo = heroType.GetField(
            "HeroCount",
            BindingFlags.NonPublic 
            | BindingFlags.Static);
        
        //设置静态字段的值
        newFieldInfo.SetValue(null,215);
        //获取静态字段的值
        string staticFieldValue = 
            newFieldInfo.GetValue(null).ToString();
        
        Debug.Log("staticFieldValue : "+ staticFieldValue);

        // heroType.GetProperties()
        
        #endregion

        #region 通过反射获取某个类的私有方法

        Framework.Hero ftml = new Framework.Hero("发条魔灵",80);
        
        //获取非静态成员方法ShowMe
        MethodInfo info = heroType.GetMethod("ShowMe",
            BindingFlags.NonPublic |
            BindingFlags.Instance,null,CallingConventions.Any,new Type[] {typeof(string)}, null);

        //调用该方法
        info.Invoke(ftml, new object[]{ "QF" });

        #endregion

        #region 通过反射获取某个类的空参数的私有方法【前提条件：有参数的私有同名方法存在】

        MethodInfo[]  methods = heroType.GetMethods(BindingFlags.NonPublic |
                            BindingFlags.Instance);

        for (int i = 0; i < methods.Length; i++)
        {
            if(methods[i].Name != "ShowMe")
                continue;
            // Debug.Log("参数个数：" + methods[i].GetParameters().Length);

            if (methods[i].GetParameters().Length == 0)
            {
                methods[i].Invoke(ftml,null);
                break;
            }
        }

        #endregion
    }

    /// <summary>
    /// 通过反射实例化对象
    /// </summary>
    private void NewObjByRef()
    {
        //获取类型
        Type heroType = typeof(Framework.Hero);

        //创建该类的实例，通过public无参构造
        // object heroObj = Activator.CreateInstance(heroType);
        // //转换成指定类型的对象
        // Framework.Hero hero = heroObj as Framework.Hero;

        //创建该类的实例，通过非公有无参构造
        Activator.CreateInstance(heroType, true);

        //创建该类的实例，通过公有有参构造
        object daysHero = Activator.CreateInstance(heroType, 31);

        Debug.Log((daysHero as Framework.Hero).GetName()); 

        //创建该类的实例，通过私有有参构造
        object privateHero = Activator.CreateInstance(heroType,
            BindingFlags.NonPublic | BindingFlags.Instance,
            null,new object[] { "QF" },
            null );
        
        Debug.Log((privateHero as Framework.Hero).attack);
    }
}