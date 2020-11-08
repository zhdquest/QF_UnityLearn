using System;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随的目标")]
    public Transform followTarget;
    [Header("跟随速度")]
    public float followSpeed = 3f;
    //摄像机指向目标的方向向量
    private Vector3 dir;
    void Start()
    {
        //计算初始的方向向量
        dir = followTarget.position - transform.position;
    }

    private void LateUpdate()
    {
        //保持跟随【直接等于】
        // transform.position = followTarget.position - dir;
        //保持跟随【Lerp】
        transform.position = Vector3.Lerp(transform.position,
            followTarget.position - dir,Time.deltaTime * followSpeed);
    }
}
