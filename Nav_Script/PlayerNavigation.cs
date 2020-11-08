using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavigation : MonoBehaviour
{
    //导航目标
    public Transform target;

    [Header("时间间隔")]
    public float offMeshLinkInterval = 3f;
    [Header("跳跃高度")]
    public float jumpHeight = 3f;
    
    private NavMeshAgent nav;

    private float linkScale = 0;

    private NavMeshPath path;
    

    private void Awake()
    {
        nav = GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
    }


    private void Start()
    {
        //设置目的地（目的地不变）
        nav.SetDestination(target.position);
        // nav.destination = target.position;
        
        //nav.Stop();//暂停导航
        //nav.Resume();//恢复

        nav.isStopped = true;//暂停导航
        nav.isStopped = false;//恢复导航
    }

    private void Update()
    {
        nav.SetDestination(target.position);

        // Debug.Log(nav.remainingDistance);

        //判断角色是否到达目的地
        if (nav.remainingDistance - nav.stoppingDistance < 0.05f)
        {
            Debug.Log("到达");
        }

        AgentOffMeshLink();

        // Debug.Log(nav.areaMask);
        // nav.areaMask = 9;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CaculatePath();
        }
    }

    //计算是否能到达指定点
    private void CaculatePath()
    {
        bool canArrive = nav.CalculatePath(Vector3.zero, path);

        Debug.Log("canArrive:" + canArrive);

        //path.corners//到达目标点的拐点
    }

    //通过脚本控制分离地面的跳跃
    private void AgentOffMeshLink()
    {
        // Debug.Log("isOnOffMeshLink:" + nav.isOnOffMeshLink);
        // nav.currentOffMeshLinkData
        //Debug.Log(nav.currentOffMeshLinkData.activated);
        //Debug.Log(nav.currentOffMeshLinkData.linkType);

        OffMeshLinkData data = nav.currentOffMeshLinkData;

        //如果此时正在往下跳
        if (nav.isOnOffMeshLink && data.linkType == OffMeshLinkType.LinkTypeManual)
        {
            //Link跳跃比例
            linkScale += Time.deltaTime / offMeshLinkInterval;
            //计算抛物线y方向的偏移量
            float upOffset = (linkScale - linkScale * linkScale) * jumpHeight;
            //设置角色的位置
            transform.position = Vector3.up + Vector3.up * upOffset + Vector3.Lerp(data.startPos, data.endPos, linkScale);

            //比例完成超过100%
            if (linkScale > 1)
            {
                //完成分离路面导航
                nav.CompleteOffMeshLink();
                linkScale = 0;
            }
        }
    }
}