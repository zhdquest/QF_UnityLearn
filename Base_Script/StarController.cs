using System;
using UnityEngine;

public class StarController : MonoBehaviour
{
    [Header("速度")]
    public float speed = 3f;
    
    private Rigidbody2D _rigidbody2D;

    private Vector3 beginPos;
    private Vector3 endPos;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnMouseEnter()
    {
        Debug.Log("当鼠标进入当前碰撞体");
    }

    private void OnMouseOver()
    {
        Debug.Log("当鼠标悬停在当前碰撞体，每帧执行一次");
    }

    private void OnMouseExit()
    {
        Debug.Log("当鼠标离开当前碰撞体");
    }

    private void OnMouseDown()
    {
        Debug.Log("当鼠标在当前碰撞体范围内按下");

        beginPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        Debug.Log("当鼠标在当前碰撞体范围内按住，每帧执行一次");
    }
    
    private void OnMouseUp()
    {
        Debug.Log("当鼠标在当前碰撞体范围内抬起");
        
        endPos = Input.mousePosition;
        Vector3 dir = beginPos - endPos;

        _rigidbody2D.velocity = dir.normalized * speed;
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("当鼠标在当前碰撞体范围内按下并抬起");
    }
}