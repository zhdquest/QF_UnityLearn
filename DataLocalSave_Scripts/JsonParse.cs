using System;
using UnityEngine;
using LitJson;//引入命名空间

[System.Serializable]
public class Hero
{
    public string 英雄名称;
    public double 生命值;
    public double 攻击力;
    public Skill[] 技能;
}

[System.Serializable]
public class Skill
{
    public string 技能名称;
    public int[] 技能冷却;
}


public class JsonParse : MonoBehaviour
{
    private string json =
        "{\"英雄名称\":\"诺克萨斯之手\",\"生命值\":100.88,\"攻击力\":30.33,\"技能\":[{\"技能名称\":\"大杀四方\",\"技能冷却\":[9,8,7,6,5]}]}";
    
    private void Start()
    {
        //将Json解析
        //非泛型/通用型解析
        JsonData data = JsonMapper.ToObject(json);
        
        Debug.Log(data["技能"][0]["技能名称"]);
        
        //泛型/指定结构解析
        Hero hero = JsonMapper.ToObject<Hero>(json);
        
        Debug.Log(hero.英雄名称);
        Debug.Log(hero.技能[0].技能名称);

        string jsonData = JsonMapper.ToJson(hero);
        Debug.Log(jsonData);

        hero = null;

        hero = JsonUtility.FromJson<Hero>(json);
        
        Debug.Log(hero.英雄名称);
        Debug.Log(hero.技能[0].技能名称);
    }
}