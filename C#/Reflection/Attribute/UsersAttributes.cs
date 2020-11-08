using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public class AuthorAttribute : Attribute
{
    public string author;

    //最后修改的日期
    private string lastDate;

    public string LastDate
    {
        get
        {
            return lastDate;
        }
        set
        {
            lastDate = value;
        }
    }

    //当前特性的构造函数
    public AuthorAttribute(string author)
    {
        this.author = author;
    }
}

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class MyConditionAttribute : Attribute
{
    public MyConditionAttribute()
    {
    }
}

[AuthorAttribute("Albert", LastDate = "2020.5.13"), Author("Tom", LastDate = "2020.10.13")]
public class UsersAttributes : MonoBehaviour
{
    public static string log = "abc";

    [MyCondition]
    public static void ShowLog()
    {
        Debug.Log(log);
    }
}

