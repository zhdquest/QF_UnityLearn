using System;
using UnityEngine;
using UIFrame;

[RequireComponent(typeof(Animator))]
public class PlayerPanel : UIModuleBase {
    
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        BindController(new PlayerModuleController());
    }

    public override void OnOpen()
    {
        base.OnOpen();
        _animator.SetBool(GameConst.OPEN_ANI_PARAMETER,true);
    }

    public override void OnClose()
    {
        base.OnClose();
        _animator.SetBool(GameConst.OPEN_ANI_PARAMETER,false);
    }
    
}