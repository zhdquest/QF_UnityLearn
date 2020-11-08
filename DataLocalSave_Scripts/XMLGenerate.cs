using UnityEngine;
using System.Xml;//引入命名空间

public class XMLGenerate : MonoBehaviour {
    
    private void Start()
    {
        //创建一个XML文档对象
        XmlDocument doc = new XmlDocument();
        //使用文档对象，创建一个头文本对象
        XmlDeclaration dec = doc.CreateXmlDeclaration(
            "1.0", "UTF-8", null);
        //将头文本节点放置到XML文档中
        doc.AppendChild(dec);
        //创建角色节点
        XmlElement root_element = doc.CreateElement("Player");

        //给根元素节点添加属性
        root_element.SetAttribute("name", gameObject.name);
        //给根元素节点添加属性
        root_element.SetAttribute("active", gameObject.activeSelf.ToString());
        
        //创建一个根元素节点[位置元素]
        XmlElement pos_Element = doc.CreateElement("Position");
        string pos = transform.position.ToString();
        //去掉括号字符[删掉最后一个字符和第一个字符]
        pos = pos.Remove(pos.Length - 1).Remove(0, 1);
        //给元素节点设置值
        pos_Element.InnerText = pos;
        //将位置元素节点添加到文档中
        root_element.AppendChild(pos_Element);
        //添加物品节点
        XmlElement bag_element = doc.CreateElement("Bag");
        //添加药品节点
        XmlElement addHp_element = doc.CreateElement("加血药");
        //设置加血药节点的值
        addHp_element.InnerText = "10";
        //将加血药设置物品节点的子节点
        bag_element.AppendChild(addHp_element);
        //将背包节点放置到文档中
        root_element.AppendChild(bag_element);
        //将根节点添加到文档
        doc.AppendChild(root_element);
        
        Debug.Log(Application.dataPath + "/Demo.xml");
        //保存文档 C:abc/Lesson17-DataSave/Assets
        doc.Save(Application.dataPath + "/Demo.xml");
    }
}