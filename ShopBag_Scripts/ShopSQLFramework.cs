using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSQLFramework : SQLFramework
{
    #region 单例 Singleton

    private static ShopSQLFramework instance;

    public static ShopSQLFramework GetInstance()
    {
        if (instance == null)
        {
            instance = new ShopSQLFramework();
        }

        return instance;
    }

    private ShopSQLFramework()
    {
    }

    #endregion

    #region Base Field

    private string sqlQuery;

    #endregion

    #region 数据库操作方法封装

    /// <summary>
    /// 查询装备价格
    /// </summary>
    /// <param name="equipName"></param>
    /// <returns></returns>
    private int SelectEquipPrice(string equipName)
    {
        //编写SQL语句
        sqlQuery = "Select EquipMoney From ShopTable Where EquipName='" + equipName + "'";
        //执行SQL语句
        object price = SelectSingleData(sqlQuery);

        //没有这个装备
        if (price == null)
        {
            //报错
            Debug.LogError("没有该装备!");
            //异常结果返回
            return -1;
        }

        //返回结果
        return Convert.ToInt32(price);
    }

    /// <summary>
    /// 获取装备数量
    /// </summary>
    public int GetHeroEquipCount(string heroName)
    {
        string equips = SelectHeroEquips(heroName);

        int count = 0;
        char[] equipsChar = equips.ToCharArray();
        for (int i = 0; i < equipsChar.Length; i++)
        {
            if (equipsChar[i] == '|')
                count++;
        }

        return count;
    }

    /// <summary>
    /// 查看英雄有多少钱
    /// </summary>
    /// <param name="heroName"></param>
    /// <returns></returns>
    public int SelectHeroMoney(string heroName)
    {
        //编写SQL语句
        sqlQuery = "Select HeroMoney From HeroTable Where HeroName='" + heroName + "'";
        
        //执行SQL语句
        object money = SelectSingleData(sqlQuery);

        if (money == null)
        {
            return -1;
        }

        return Convert.ToInt32(money);
    }

    /// <summary>
    /// 设置召唤师的钱数
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="newMoney"></param>
    private void SetHeroMoney(string heroName, int newMoney)
    {
        //编写SQL语句
        
        // sqlQuery = "Update HeroTable Set HeroMoney="
        //            + newMoney + " Where HeroName='" + heroName + "'";
        
        sqlQuery = string.Format(
            "Update HeroTable Set HeroMoney={0} Where HeroName='{1}'",
            newMoney, heroName);
        //执行SQL语句
        JustExecute(sqlQuery);
    }

    /// <summary>
    /// 花费召唤师的金钱
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="cost"></param>
    /// <returns>如果为true则可以完成消费，否则无法完成消费</returns>
    private bool CostHeroMoney(string heroName,int cost)
    {
        int heroMoney = SelectHeroMoney(heroName);

        if (heroMoney >= cost)
        {
            //花钱
            SetHeroMoney(heroName, heroMoney - cost);
            return true;
        }

        return false;
    }

    /// <summary>
    /// 查询英雄装备列表
    /// </summary>
    /// <param name="heroName"></param>
    /// <returns></returns>
    public string SelectHeroEquips(string heroName)
    {
        //编写SQL语句
        sqlQuery = "Select HeroEquips From HeroTable Where HeroName='" + heroName +"'";
        //执行SQL语句
        object equips = SelectSingleData(sqlQuery);

        if (equips == null)
        {
            Debug.Log("没有该英雄!");
            return null;
        }

        return equips.ToString();
    }

    /// <summary>
    /// 给英雄添加装备
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="equipName"></param>
    private void AddHeroEquip(string heroName,string equipName)
    {
        //查询英雄装备
        string heroEquips = SelectHeroEquips(heroName);

        if (heroEquips != null)
        {
            //添加新装备
            heroEquips += equipName + "|";
        }

        SetHeroEquips(heroName, heroEquips);
    }

    /// <summary>
    /// 设置英雄装备字符串
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="heroEquips"></param>
    private void SetHeroEquips(string heroName,string heroEquips)
    {
        //更新到数据库
        sqlQuery = "Update HeroTable Set HeroEquips='" 
                   + heroEquips + "' Where HeroName = '" 
                   + heroName + "'";
        //执行
        JustExecute(sqlQuery);
    }

    /// <summary>
    /// 查询装备的属性信息
    /// </summary>
    /// <param name="equipName"></param>
    private int[] SelectEquipProperties(string equipName)
    {
        //编写SQL语句
        sqlQuery = "Select * From ShopTable Where EquipName = '" + equipName + "'";
        //执行语句
        List<ArrayList> properties = SelectMultipleData(sqlQuery);
        //结果数组
        int[] result = new int[4];
        //数据存在
        if (properties.Count != 0)
        {
            for (int i = 2; i <= 5; i++)
            {
                //将结果储存
                result[i - 2] = Convert.ToInt32(properties[0][i]);
            }

            // for (int i = 0; i < 4; i++)
            // {
            //     result[i] = Convert.ToInt32(properties[0][i + 2]);
            // }
            //返回结果
            return result;
        }
        //返回空
        return null;
    }

    /// <summary>
    /// 查询英雄的属性信息
    /// </summary>
    /// <param name="heroName"></param>
    /// <returns></returns>
    public int[] SelectHeroProperties(string heroName)
    {
        //编写SQL语句
        sqlQuery = "Select * From HeroTable Where HeroName = '" + heroName + "'";
        //执行语句
        List<ArrayList> properties = SelectMultipleData(sqlQuery);
        //结果数组
        int[] result = new int[4];
        //数据存在
        if (properties.Count != 0)
        {
            for (int i = 2; i <= 5; i++)
            {
                //将结果储存
                result[i - 2] = Convert.ToInt32(properties[0][i]);
            }

            // for (int i = 0; i < 4; i++)
            // {
            //     result[i] = Convert.ToInt32(properties[0][i + 2]);
            // }
            //返回结果
            return result;
        }
        //返回空
        return null;
    }

    /// <summary>
    /// 给英雄添加一个装备属性
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="equipName"></param>
    private void AddHeroEquipProperties(string heroName,string equipName)
    {
        //获取英雄属性
        int[] heroProperties = SelectHeroProperties(heroName);
        //获取装备属性
        int[] equipProperties = SelectEquipProperties(equipName);
        //叠加
        for (int i = 0; i < 4; i++)
        {
            //属性加成
            heroProperties[i] += equipProperties[i];
        }

        SetHeroProperties(heroName,heroProperties);
    }

    /// <summary>
    /// 设置英雄属性
    /// </summary>
    /// <param name="heroName"></param>
    /// <param name="heroProperties"></param>
    private void SetHeroProperties(string heroName,int[] heroProperties)
    {
        //更新英雄属性到数据库
        sqlQuery = string.Format("Update HeroTable Set HeroAD = {0}," +
                                 "HeroAP={1}," +
                                 "HeroAR={2}," +
                                 "HeroSR={3} Where HeroName='" + heroName + "'",
            heroProperties[0],heroProperties[1],heroProperties[2],heroProperties[3]);
        //执行语句
        JustExecute(sqlQuery);
    }

    /// <summary>
    /// 查询英雄名称
    /// </summary>
    public string SelectHeroName(int index)
    {
        //编写SQL语句
        sqlQuery = "Select HeroName From HeroTable";
        //执行语句
        List<ArrayList> heros = SelectMultipleData(sqlQuery);
        //返回名称
        return heros[index][0].ToString();
    }

    /// <summary>
    /// 查询商店里的所有装备
    /// </summary>
    /// <returns></returns>
    public string[] SelectAllShopEquips()
    {
        //编写语句
        sqlQuery = "Select * From ShopTable";
        //执行
        List<ArrayList> result = SelectMultipleData(sqlQuery);
        //创建数组
        string[] equipNames = new string[result.Count];

        //获取所有名称的
        for (int i = 0; i < result.Count; i++)
        {
            equipNames[i] = result[i][0].ToString();
        }
        
        return equipNames;
    }

    #endregion

    public void SellEquip(string heroName, string equipName)
    {
        //出售流程：
        //1、查询该装备的价格【查】【查一个】
        int equipPrice = SelectEquipPrice(equipName);
        //2、查询当前召唤师有多少钱【查】【查一个】
        int heroMoney = SelectHeroMoney(heroName);
        //4、加钱：召唤师的钱+=装备的钱/2【改】
        SetHeroMoney(heroName, heroMoney + equipPrice / 2);
        //5、查询英雄现有哪些装备【查】【查一个】
        string heroEquips = SelectHeroEquips(heroName);
        //6、给英雄移除该装备【改】【无尽之刃|饮血剑|暴风大剑|】
        //6.1查询装备字符串在整个装备字符串中的位置
        int index = heroEquips.LastIndexOf(equipName);
        //6.2删除装备及|
        heroEquips = heroEquips.Remove(index, equipName.Length + 1);
        //6.3更新到数据库
        SetHeroEquips(heroName,heroEquips);
        //7、查询该装备的属性加成【查】【查多个】
        int[] equipProperties = SelectEquipProperties(equipName);
        //8、查询当前英雄的属性值【查】【查多个】
        int[] heroProperties = SelectHeroProperties(heroName);
        //9、英雄属性-=装备属性加成
        for (int i = 0; i < heroProperties.Length; i++)
        {
            heroProperties[i] -= equipProperties[i];
        }
        //10、更新英雄属性【改】
        SetHeroProperties(heroName,heroProperties);
    }

    public void BuyEquip(string heroName, string equipName)
    {
        //购买流程：
        //1、查询该装备的价格【查】【查一个】
        int price = SelectEquipPrice(equipName);
        //2、查询当前召唤师有多少钱【查】【查一个】
        //3、判断钱够不够
        //4、扣钱：召唤师的钱-=装备的钱【改】
        if (!CostHeroMoney(heroName, price))
        {
            Debug.Log("钱不够！");
            return;
        }
        //5、查询英雄现有哪些装备【查】【查一个】
        //6、给英雄添加该装备【改】
        AddHeroEquip(heroName,equipName);
        //7、查询该装备的属性加成【查】【查多个】
        //8、查询当前英雄的属性值【查】【查多个】
        //9、英雄属性+=装备属性加成
        //10、更新英雄属性【改】
        AddHeroEquipProperties(heroName,equipName);
    }
}