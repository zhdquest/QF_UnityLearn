//using NUnit.Framework.Constraints;
using UIFrame;
using UnityEngine;

public class MainModule : UIModuleBase
{
    protected void Start()
    {
        //绑定控制器
        BindController(new MainModuleController());
    }

    public override void OnOpen()
    {
        base.OnOpen();
    }

    public override void OnClose()
    {
        base.OnClose();
    }

    public override void OnPause()
    {
        base.OnPause();
    }

    public override void OnResume()
    {
        base.OnResume();
    }
}