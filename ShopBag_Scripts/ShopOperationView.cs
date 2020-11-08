using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopOperationView : MonoBehaviour
{
    [Header("召唤师名称")]
    public Text heroNameText;
    [Header("英雄属性")]
    public Text[] heroPropertiesText;
    [Header("召唤师金钱")]
    public Text heroMoneyText;
    [Header("背包")]
    public Transform bagTra;
    [Header("商城")]
    public Transform shopTra;

    public string heroName;

    private ShopSQLFramework shopOp;

    private GameObject bagEquipPrefab;
    private GameObject shopEquipPrefab;

    //当前脚本的静态索引
    public static ShopOperationView instance;
    
    private void Awake()
    {
        instance = this;
        shopOp = ShopSQLFramework.GetInstance();
    }

    private void Start()
    {
        bagEquipPrefab = GetResourcesPrefab("Prefabs/BagEquip");
        shopEquipPrefab = GetResourcesPrefab("Prefabs/ShopEquip");
        
        shopOp.OpenDatabase("ShopDatabase");
        
        ShowShopEquips();
        UpdateView();
    }

    private void OnApplicationQuit()
    {
        shopOp.CloseDatabase();
    }

    /// <summary>
    /// 显示商店里的所有装备
    /// </summary>
    private void ShowShopEquips()
    {
        //获取所有装备的名称
        string[] names = shopOp.SelectAllShopEquips();

        for (int i = 0; i < names.Length; i++)
        {
            GameObject crtEquip = Instantiate(shopEquipPrefab);
            
            //设置父物体
            crtEquip.transform.SetParent(shopTra.GetChild(i));
            //设置相对坐标
            crtEquip.transform.localPosition = Vector3.zero;
            //设置相对缩放
            crtEquip.transform.localScale = Vector3.one;
            //设置当前装备的图片
            crtEquip.GetComponent<ShopEquip>().SetEquipSprite(names[i]);
        }
    }

    /// <summary>
    /// 更新视图
    /// </summary>
    public void UpdateView()
    {
        //获取英雄名称
        heroName = shopOp.SelectHeroName(0);
        //显示名称
        heroNameText.text = heroName;
        //获取英雄属性
        int[] heroProperties = shopOp.SelectHeroProperties(heroName);
        //显示英雄属性
        for (int i = 0; i < 4; i++)
        {
            heroPropertiesText[i].text = heroProperties[i].ToString();
        }
        //获取金钱
        int heroMoney = shopOp.SelectHeroMoney(heroName);
        //显示金钱
        heroMoneyText.text = heroMoney.ToString();
        //获取英雄装备列表【无尽之刃|冥火之拥|饮血剑|】
        string equips = shopOp.SelectHeroEquips(heroName);
        
        //清空背包的所有装备
        for (int i = 0; i < bagTra.childCount - 1; i++)
        {
            if (bagTra.GetChild(i).childCount > 0)
            {
                //销毁原来的装备图片
                Destroy(bagTra.GetChild(i).GetChild(0).gameObject);
            }
        }
        
        //获取每个装备的名称
        string[] equipNames = equips.Split('|');
        
        for (int i = 0; i < equipNames.Length; i++)
        {
            //如果装备名称不正常，跳过
            if(string.IsNullOrEmpty(equipNames[i]))
                continue;
            //生成背包装备
            GameObject crtEquip = Instantiate(bagEquipPrefab);
            //设置父物体
            crtEquip.transform.SetParent(bagTra.GetChild(i));
            //设置相对坐标
            crtEquip.transform.localPosition = Vector3.zero;
            //设置相对缩放
            crtEquip.transform.localScale = Vector3.one;
            //设置当前装备的图片
            crtEquip.GetComponent<BagEquip>().SetEquipSprite(equipNames[i]);
        }
    }

    /// <summary>
    /// 从Resource里面加载预设体
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    private GameObject GetResourcesPrefab(string path)
    {
        return Resources.Load<GameObject>(path);
    }
}