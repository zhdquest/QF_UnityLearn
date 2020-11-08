using System;
using UnityEngine;


public class Task
{
    private int taskTargetCount;

    private int curretnCount;

    public void SetTarget(int targetCount)
    {
        this.taskTargetCount = targetCount;
    }

    public void TaskCallback()
    {
        curretnCount++;
        Debug.Log("检测到抽取了一张牌("+ 
                  curretnCount + "/"+ taskTargetCount + ")");
        
        if (curretnCount == taskTargetCount)
        {
            Debug.Log("抽牌任务完成..");
        }
    }
}

public class CardManager
{
    private Action getCardCallbacks;

    public void AddGetCardListener(Action action)
    {
        if (getCardCallbacks == null)
        {
            getCardCallbacks = action;
        }
        else
        {
            getCardCallbacks += action;
        }
    }

    public void RemoveGetCardListener(Action action)
    {
        try
        {
            getCardCallbacks -= action;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e);
        }
    }

    public void GetOneCard()
    {
        if (getCardCallbacks != null)
        {
            getCardCallbacks();
        }
    }

    public void GetOneCard(Action action)
    {
        Debug.Log("第一阶段完成...");
        action();
        Debug.Log("第二阶段完成...");
        action();
        Debug.Log("第三阶段完成...");
        action();
    }
}

public class ProxyDemo : MonoBehaviour
{
    private CardManager cardManager;

    private Action _action;
    
    private void Start()
    {
        cardManager = new CardManager();
        Task task = new Task();
        //设置任务目标5张牌
        task.SetTarget(5);
        //绑定监听
        cardManager.AddGetCardListener(task.TaskCallback);
        
        _action = () => { Debug.Log("第一次绑定委托"); };
        _action += () => { Debug.Log("第二次绑定委托"); };
        _action += () => { Debug.Log("第三次绑定委托"); };
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //抽一张牌
            // cardManager.GetOneCard();
            
            cardManager.GetOneCard(_action);
        }
    }
}