using UnityEngine;
using System.Collections;
using UIFrame;
using UnityEngine.UI;
using Utilty;
using EventType = Utilty.EventType;

public class MainModuleController : UIControllerBase {
    
    private RectTransform _rectTransform;
    /// <summary>
    /// 控制器启动
    /// </summary>
    /// <param name="module"></param>
    public override void ControllerStart(UIModuleBase module)
    {
        base.ControllerStart(module);
        _rectTransform = _module.GetComponent<RectTransform>();
        ControllerInit();
        TaskButtonDemoMethod();
        AddListerDemo();
    }

    private void AddListerDemo()
    {
        EventCenter.GetInstance().AddListener<Color>(EventType.GetTask,OnGetTask);
    }

    private void OnGetTask(Color color)
    {
        Transform parent = _module.FindCurrentModuleWidget("ButtomButtons*")._Transform;

        for (int i = 0; i < parent.childCount; i++)
        {
            //变色
            parent.GetChild(i).GetComponent<Image>().color = color;
        }
    }

    private void ControllerInit()
    {
        _rectTransform.offsetMax = Vector2.zero;
        _rectTransform.offsetMin = Vector2.zero;
    }
    private void TaskButtonDemoMethod()
    {
        UIWidgetBase taskBtn = _module.FindCurrentModuleWidget("TaskButton#");
        
        taskBtn._Button.onClick.AddListener(() =>
        {
            UIManager.GetInstance().PushModule("TaskPanel");
            
            taskBtn._Image.color = new Color(
                Random.Range(0f,1f),
                Random.Range(0f,1f),
                Random.Range(0f,1f));

            // UIWidgetBase btn = UIManager.GetInstance().FindWidget("TaskPanel", "AccecptButton#");
            //
            // btn._Image.color = new Color(
            //     Random.Range(0f,1f),
            //     Random.Range(0f,1f),
            //     Random.Range(0f,1f));
        });
    }
}