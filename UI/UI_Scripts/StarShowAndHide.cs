using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StarShowAndHide : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    private float mouseX;
    private float mouseY;

    private Image[] allStar;

    private void Awake()
    {
        //实例化
        allStar = new Image[transform.parent.childCount];

        for (int i = 0; i < allStar.Length; i++)
        {
            allStar[i] = transform.parent.GetChild(i).GetComponent<Image>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if ( mouseX > 0)
        {
            int index = transform.GetSiblingIndex();

            //记录得到的星星数量
            GameManager.GetInstance().currentStars = index + 1;
            
            //显示星星
            for (int i = 0; i <= index; i++)
            {
                allStar[i].color = Color.white;
            }
            //隐藏星星
            for (int i = index + 1; i < allStar.Length; i++)
            {
                allStar[i].color = new Color(1,1,1,0);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        if (/*Mathf.Abs(mouseX) > Mathf.Abs(mouseY) && */mouseX < 0)
        {
            int index = transform.GetSiblingIndex();

            for (int i = index; i < allStar.Length; i++)
            {
                allStar[i].color = new Color(1,1,1,0);
            }
            
            //记录得到的星星数量
            GameManager.GetInstance().currentStars = index;
        }
    }
}