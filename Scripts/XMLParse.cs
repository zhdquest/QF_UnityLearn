using System;
using UnityEngine;
using System.Xml;//引入命名空间

public class XMLParse : MonoBehaviour {
    
    private void Start()
    {
        Parse();
    }

    private void Parse()
    {
        //创建一个文档对象
        XmlDocument doc = new XmlDocument();
        //加载XML到文档对象
        doc.Load(Application.dataPath + "/Demo.xml");
        //解析XML
        //获取到第一个子节点【是XML的头文本节点】
        XmlDeclaration node = doc.FirstChild as XmlDeclaration;
        //获取某一个节点
        XmlElement pos_element = doc.SelectSingleNode("Player/Position") as XmlElement;
        //打印该节点的值
        string pos_Text = pos_element.InnerText;
        Debug.Log(pos_Text);
        //11,22,33
        string[] pos_xyz = pos_Text.Split(',');
        //转换为Vector3
        Vector3 pos = new Vector3(
            Convert.ToSingle(pos_xyz[0]),
            Convert.ToSingle(pos_xyz[1]),
            Convert.ToSingle(pos_xyz[2]));
        //设置当对象的坐标
        transform.position = pos;

        Debug.Log("------------------------------");

        //解析多个节点
        XmlNodeList list = doc.SelectNodes("Player/*");
        foreach (XmlElement item in list)
        {
            Debug.Log(item.Name);
            Debug.Log(item.InnerText);
        }

        //获取player根元素
        XmlElement player_element = doc.SelectSingleNode("Player") as XmlElement;

        string name = player_element.GetAttribute("name");
        string active = player_element.GetAttribute("active");
        
        Debug.Log(name);
        Debug.Log(active);

        gameObject.name = name;
    }

    private void UpdateXMLPos()
    {
        //创建一个文档对象
        XmlDocument doc = new XmlDocument();
        //加载XML到文档对象
        doc.Load(Application.dataPath + "/Demo.xml");
        //得到元素节点
        XmlElement pos_element = doc.SelectSingleNode("Player/Position") as XmlElement;
        //给位置元素设置新的文本值
        pos_element.InnerText = "100,0,300";
        //重新保存文档
        doc.Save(Application.dataPath + "/Demo.xml");
    }

    private void DeleteXMLPos()
    {
        //创建一个文档对象
        XmlDocument doc = new XmlDocument();
        //加载XML到文档对象
        doc.Load(Application.dataPath + "/Demo.xml");
        //得到根元素节点
        XmlElement player_element = doc.SelectSingleNode("Player") as XmlElement;
        //得到位置元素节点【想要删除的节点】
        XmlElement pos_element = doc.SelectSingleNode("Player/Position") as XmlElement;
        //移除节点
        player_element.RemoveChild(pos_element);
        //重新保存文档
        doc.Save(Application.dataPath + "/Demo.xml");
    }
}