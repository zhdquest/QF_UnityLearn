using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HeroEquip : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [Header("装备类型")]
    public EquipType equipType = EquipType.Helmet;
    
    private Image crtImage;
    private Transform parent;
    

    private void Awake()
    {
        crtImage = GetComponent<Image>();
    }

    private void Start()
    {
        //记录原来的父对象
        parent = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //记录原来的父对象
        parent = transform.parent;
        //设置为画布的子对象
        transform.SetParent(transform.root);
        //取消射线检测
        crtImage.raycastTarget = false;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //标签为Box【BagBox，EquipBox】
        if (eventData.pointerEnter.CompareTag("Box") && eventData.pointerEnter.transform.childCount == 0)
        {
            //格子去接收
            BoxReceive(eventData.pointerEnter.GetComponent<EquipBox>());
        }
        //标签为Equip【BoxEquip,EquipEquip】
        else if(eventData.pointerEnter.CompareTag("Equip"))
        {
            //交换
            Exchange(eventData.pointerEnter);
        }
        //Other
        else
        {
            BackToOrigin();
        }
        //恢复射线检测
        crtImage.raycastTarget = true;
    }

    /// <summary>
    /// 交换
    /// </summary>
    /// <param name="buttomObj"></param>
    private void Exchange(GameObject buttomObj)
    {
        //下面的装备
        HeroEquip buttomEquip = buttomObj.GetComponent<HeroEquip>();
        //获取下面装备的父物体
        EquipBox buttonEqupBox = buttomObj.transform.parent.GetComponent<EquipBox>();
        //当前格子的父对象
        EquipBox topEqupBox = parent.GetComponent<EquipBox>();

        //两个装备目前都在背包里
        if ((buttonEqupBox.equipType != EquipType.None ||
             topEqupBox.equipType != EquipType.None)
            &&(buttomEquip.equipType != equipType))
        {
            //回到原始位置
            BackToOrigin();
        }
        else
        {
            //下面的格子接收当前装备
            buttonEqupBox.SetEquip(this);
            //上面所在的格子接收下面的装备
            topEqupBox.SetEquip(buttomEquip);
        }
    }

    /// <summary>
    /// 格子接收装备
    /// </summary>
    private void BoxReceive(EquipBox box)
    {
        //调用格子的设置方法
        box.SetEquip(this);
    }

    /// <summary>
    /// 回归原始位置
    /// </summary>
    public void BackToOrigin()
    {
        SetParent(parent);
    }

    /// <summary>
    /// 设置到某个格子里
    /// </summary>
    /// <param name="box"></param>
    public void SetToBox(Transform box)
    {
        SetParent(box);
    }

    /// <summary>
    /// 设置新的父物体
    /// </summary>
    /// <param name="newParent"></param>
    private void SetParent(Transform newParent)
    {
        //将原父对象还设置回来
        transform.SetParent(newParent);
        //调整本地坐标
        transform.localPosition = Vector3.zero;
    }
}