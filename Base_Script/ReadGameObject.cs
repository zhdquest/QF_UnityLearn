using UnityEngine;

public class Person
{
    private string name;
    private int age;

    public Person(string name, int age)
    {
        this.name = name;
        this.age = age;
    }

    public override string ToString()
    {
        return name + "：" + age;
    }
}

public class ReadGameObject : MonoBehaviour
{
    void Start()
    {
        #region Members

        //当前游戏对象
        // this.gameObject
        //只读属性，描述游戏对象的激活状态
        Debug.Log(gameObject.activeSelf);
        
        //设置当前游戏对象的激活状态
        gameObject.SetActive(false);
        
        //设置当前游戏对象的激活状态为当前状态的反向状态
        
        gameObject.SetActive(!gameObject.activeSelf);
        
        // if (gameObject.activeSelf)
        // {
        //     gameObject.SetActive(false);
        // }
        // else
        // {
        //     gameObject.SetActive(true);
        // }

        Debug.Log(gameObject.name);
        Debug.Log(gameObject.tag);//标签
        Debug.Log(gameObject.layer);

        Light myLight = gameObject.GetComponent("Light") as Light;
        myLight = gameObject.GetComponent(typeof(Light)) as Light;
        //最好用，做常用
        myLight = gameObject.GetComponent<Light>();

        //给当前游戏对象添加一个T类型组件
        myLight = gameObject.AddComponent<Light>();
        
        Debug.Log(myLight);
        
        #endregion

        #region Static

        //通过名字找到单个游戏对象【不推荐】(写Demo)
        GameObject lt = GameObject.Find("Directional Light");
        Debug.Log(lt);
        //通过标签找到单个游戏对象【推荐】
        GameObject cam = GameObject.FindWithTag("MainCamera");
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        
        //通过标签找到多个游戏对象
        GameObject[] cams = GameObject.FindGameObjectsWithTag("MainCamera");
        
        #endregion

    }
}
