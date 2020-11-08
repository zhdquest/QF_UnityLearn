using System.Collections.Generic;
using UnityEngine;

//class Hero
//{
    
//    //特征
//    public string name = "xiaoming";
//    public bool isMan = true;
//    public char sex = 'M';


//    //行为

//}

public class Singleton
{
    private Singleton()
    {
    }
}


//类 方法 属性 字段 委托
public class Class
{
    //所有字段
    public Dictionary<string, object> fields;
    
    //.....

    private string equip01;
    private string equip02;
    private string equip03;
    private string equip04;
    private string equip05;
    private string equip06;

}

public class Method {
    
    
    
}

public class BasicRef : MonoBehaviour {
    private void Start()
    {
        Hero anni = new Hero();

        for (int i = 1; i < 7; i++)
        {
            string equipFieldName = "equip0" + i.ToString();
            
        }
        
        //anni --> Hero
        
        //anni  -- 类别
        //        -- 属性、字段
        //        -- 方法、委托、事件
        
        //Hero类
        Class hero = new Class();
        //Person类
        Class person = new Class();
        
        
    }
}