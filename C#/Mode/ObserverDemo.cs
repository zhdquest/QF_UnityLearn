using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObserverAb
{
    public abstract void ReceiveMsg(string msg);
}

public abstract class SubjectAb
{
    protected IList<ObserverAb> observers;
    public abstract void AddObserver(ObserverAb observer);
    public abstract void RemoveObserver(ObserverAb observer);
    public abstract void Notify(string msg);
}



/// <summary>
/// 观察者等待消息
/// </summary>
public class ObserverWaitMsg : ObserverAb
{
    public string name;

    public ObserverWaitMsg(string name)
    {
        this.name = name;
    }

    public override void ReceiveMsg(string msg)
    {
        Debug.Log(name + "收到了消息：" + msg);
    }
}



/// <summary>
/// 通知者发送消息
/// </summary>
public class SubjectSendMsg : SubjectAb
{
    public SubjectSendMsg()
    {
        observers = new List<ObserverAb>();
    }

    public override void AddObserver(ObserverAb observer)
    {
        //判断是否有该观察者
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public override void RemoveObserver(ObserverAb observer)
    {
        //判断是否有该观察者
        if (observers.Contains(observer))
        {
            observers.Remove(observer);
        }
    }

    /// <summary>
    /// 通知方法
    /// </summary>
    /// <param name="msg"></param>
    public override void Notify(string msg)
    {
        for (int i = 0; i < observers.Count; i++)
        {
            //通知给每个观察者
            observers[i].ReceiveMsg(msg);
        }
    }

    /// <summary>
    /// 观察游戏时间
    /// </summary>
    public void WatchGameTime(float targetTime,float currentTime)
    {
        if (currentTime >= targetTime)
        {
            Notify("挂机任务完成了...");
        }
    }
}

public class ObserverDemo : MonoBehaviour
{
    //普通任务观察者
    private ObserverWaitMsg normalTask;
    //成就任务观察者
    private ObserverWaitMsg achievementTask;

    //通知者
    private SubjectSendMsg subject;

    private void Start()
    {
        normalTask = new ObserverWaitMsg("普通任务观察者");
        achievementTask = new ObserverWaitMsg("成就任务观察者");
        
        subject = new SubjectSendMsg();
        
        //添加观察者，到观察者列表
        subject.AddObserver(normalTask);
        subject.AddObserver(achievementTask);

        //移除成就任务观察者
        subject.RemoveObserver(achievementTask);
    }

    private void Update()
    {
        //通知者（具体观察者）每帧进行时间监测
        subject.WatchGameTime(5,Time.time);
    }
}