using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_GameObject : MonoBehaviour
{
    GameObject theGameObject;
    void _Active()
    {
        //激活或停用该游戏对象
        //GameObject 可能因为父项未处于活动状态而处于非活动状态。在这种情况下，调用 SetActive 不会激活它
        theGameObject.SetActive(true);
        //返回此 GameObject 的本地活动状态，只读
        //GameObject 可能因为父项未处于活动状态而处于非活动状态，即使其返回 true 也是如此
        //要检查此 GameObject 实际上在该场景中是否被视为处于活动状态，可使用 GameObject.activeInHierarchy
        bool isActive = theGameObject.activeSelf;
    }

    void _Info()
    {
        string name=theGameObject.name;
        string tag = theGameObject.tag;
        int layer = theGameObject.layer;
    }

    void _Component()
    {
        //如果游戏对象附加了类型为 type 的组件，则将其返回，否则返回 null。
        theGameObject.GetComponent(typeof(Rigidbody));
        //使用泛型，效果同上
        theGameObject.GetComponent<Rigidbody>();

        //将某类型的组件类添加到该游戏对象
        theGameObject.AddComponent<Rigidbody>();
    }

    //不能找到非激活对象
    void _Find()
    {
        //Find为静态方法
        //按 name 查找 激活的GameObject，然后返回它
        //如果 name 包含“/”字符，则会向路径名称那样遍历此层级视图。
        //将在所有这些场景中进行搜索，出于性能原因，建议不要每帧都使用此函数，而是在启动时将结果缓存到成员变量中。
        //如果您要查找子 GameObject，使用 Transform.Find 通常会更加轻松。
        GameObject.Find("name");
        //返回一个标记为 tag 的激活GameObject。如果未找到 GameObject，则返回 null。
        //标签在使用前必须在标签管理器中加以声明。如果此标签不存在，或者传递了空字符串或 null 作为标签，则将抛出 UnityException
        GameObject.FindWithTag("tag");
        //返回标记为 tag 的激活的GameObject 的列表。如果未找到 GameObject，则返回空数组。
        GameObject.FindGameObjectsWithTag("");
    }
}
