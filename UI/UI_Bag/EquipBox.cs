using UnityEngine;

public enum EquipType
{
    None,
    Helmet,
    Ring,
    Clothes,
    Weapon,
    Shoes
}

public class EquipBox : MonoBehaviour
{
    [Header("装备栏类型")]
    public EquipType equipType = EquipType.None;

    /// <summary>
    /// 设置装备到当前格子
    /// </summary>
    public void SetEquip(HeroEquip heroEquip)
    {
        //如果当前格子没有类型限制，或，当前格子的类型限制与装备类型匹配
        if (equipType == EquipType.None || equipType == heroEquip.equipType)
        {
            //设置进去
            heroEquip.SetToBox(transform);
        }
        else
        {
            //回调原始位置
            heroEquip.BackToOrigin();
        }
    }
}