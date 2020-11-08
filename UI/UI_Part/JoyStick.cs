using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    [Header("摇杆移动半径")]
    public float moveRadius = 50;
    
    //初始坐标
    private Vector3 originPos;

    //private PlayerController _playerController;

    private void Awake()
    {
       // _playerController = GameObject.FindWithTag(
            //"Player").GetComponent<PlayerController>();
    }

    private void Start()
    {
        //获取初始坐标
        originPos = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        //临时位置
        Vector3 tempWorldPos = Vector3.one;
        Vector2 tempLocalPos = Vector2.one;
        //获取画布
        RectTransform canvas = transform.parent as RectTransform;
        //将屏幕坐标转为世界UI坐标
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            canvas, new Vector2(Input.mousePosition.x,Input.mousePosition.y), Camera.main, out tempWorldPos);
        //将屏幕坐标转为本地UI坐标
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas, new Vector2(Input.mousePosition.x, Input.mousePosition.y), Camera.main, out tempLocalPos);
        //此时摇杆与初始坐标的距离
        float dis = Vector3.Distance(originPos, tempWorldPos);
        //求初始坐标指向当前鼠标坐标的方向向量
        Vector3 dir = tempWorldPos - originPos;
        
        //给玩家赋值方向
        //_playerController.dir = new Vector3(dir.x,0,dir.y);
        
        //如果摇杆在圆盘范围内
        if (dis < moveRadius)
        {
            //拖拽移动【第一种画布模式】
            // transform.position = Input.mousePosition;
            //拖拽移动【第二种画布模式】
            // transform.position = tempWorldPos;
            transform.localPosition = tempLocalPos;
        }
        else
        {
            //①求该向量的单位向量，然后再求目标向量
            // transform.position = dir.normalized * moveRadius + originPos;
            //②直接求该向量的插值坐标
            transform.position = Vector3.Lerp(originPos, tempWorldPos, moveRadius / dis);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //回归原始坐标
        transform.position = originPos;
        
        //让玩家停下来
        //_playerController.dir = Vector3.zero;
    }
}