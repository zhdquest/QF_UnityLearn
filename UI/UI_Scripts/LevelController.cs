using System;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class LevelController : MonoBehaviour,IPointerClickHandler
{
    private GameObject starsBg;
    private GameObject stars;
    private GameObject lockObj;
    private IPointerClickHandler _pointerClickHandlerImplementation;

    private Transform selected;

    private void Awake()
    {
        selected = GameObject.FindWithTag("Selected").transform;
        starsBg = transform.GetChild(1).gameObject;
        stars = transform.GetChild(2).gameObject;
        lockObj = transform.GetChild(3).gameObject;
    }

    /// <summary>
    /// 解锁关卡
    /// </summary>
    public void UnlockLevel(bool isNew)
    {
        lockObj.SetActive(false);
        if (!isNew)
        {
            starsBg.SetActive(true);
            stars.SetActive(true);
        }
    }

    /// <summary>
    /// 设置关卡星星数量
    /// </summary>
    /// <param name="starCount"></param>
    public void SetStar(int starCount)
    {
        //显示
        for (int i = 0; i < starCount; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(true);
        }
        //隐藏
        for (int i = starCount; i < stars.transform.childCount; i++)
        {
            stars.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!lockObj.activeSelf)
        {
            //设置为当前对象的子对象
            selected.SetParent(transform);
            //设置本地坐标
            selected.localPosition = Vector3.zero;
            //设置关卡编号
            GameManager.GetInstance().levelIndex = transform.GetSiblingIndex();
        }
    }
}