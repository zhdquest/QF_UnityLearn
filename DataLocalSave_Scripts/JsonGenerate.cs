using UnityEngine;
using System.Json;

public class JsonGenerate : MonoBehaviour {
    
    private void Start()
    {
        //创建Json根对象
        JsonObject rootObj = new JsonObject();
        
        rootObj.Add("英雄名称","诺克萨斯之手");
        rootObj.Add("生命值",100.88f);
        rootObj.Add("攻击力",30.33f);

        JsonArray skills = new JsonArray();
        
        JsonObject skillQ = new JsonObject();
        skillQ.Add("技能名称","大杀四方");
        JsonArray cds = new JsonArray(9,8,7,6,5);
        skillQ.Add("技能冷却",cds);
        skills.Add(skillQ);
        rootObj.Add("技能",skills);
        
        Debug.Log(rootObj.ToString());
    }
}