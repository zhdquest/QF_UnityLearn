using UnityEngine;
using System.Collections;
using UIFrame;
using Utilty;
using EventType = Utilty.EventType;

public class TaskModuleController : UIControllerBase
{
    public override void ControllerStart(UIModuleBase module)
    {
        base.ControllerStart(module);

        _module.FindCurrentModuleWidget("AccecptButton#")
            ._Button.onClick.AddListener(OnAccecptButtonClick);
    }

    private void OnAccecptButtonClick()
    {
        //出栈
        UIManager.GetInstance().PopModule();

        //调用事件中心的事件
        EventCenter.GetInstance().Call(EventType.GetTask, Color.red);

        UILocalizationTextManager.GetInstance().LocalizationTextChange(0);

    }
}