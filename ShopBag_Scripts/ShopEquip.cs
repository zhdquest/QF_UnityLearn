using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShopEquip : MonoBehaviour {
    
    private Image _image;

    private Button _button;

    /// <summary>
    /// 设置装备的图片
    /// </summary>
    /// <param name="equipName"></param>
    public void SetEquipSprite(string equipName)
    {
        //找到Image
        _image = GetComponent<Image>();
        //找到Button
        _button = GetComponent<Button>();
        //动态加载图片
        _image.sprite = Resources.Load<Sprite>("Textures/" + equipName);
        //设置当前按钮的点击事件
        _button.onClick.AddListener(() =>
        {
            if(ShopSQLFramework.GetInstance().GetHeroEquipCount(
                    ShopOperationView.instance.heroName) == 8)
                return;
            //买装备的数据库操作
            ShopSQLFramework.GetInstance().BuyEquip(
                ShopOperationView.instance.heroName,/*英雄名称*/
                _image.sprite.name);
            //更新数据库内容到界面
            ShopOperationView.instance.UpdateView();
        });
    }
}