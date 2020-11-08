using UnityEngine;
using UnityEngine.UI;


interface IBaseOperation
{
    void Move();
    void Jump();
}

interface IFire
{
    void Fire();
}

interface IUseSkill
{
    void UseSkill();
}

interface IBaseRole : IUseSkill,IFire,IBaseOperation
{
}



public class PlayerRole : IBaseRole
{
    public void UseSkill()
    {
    }

    public void Fire()
    {
    }

    public void Move()
    {
    }

    public void Jump()
    {
    }
}

#region ISP

public class ISPPlayer : IBaseOperation
{
    public void Move()
    {
    }

    public void Jump()
    {
    }
}

public class NewISPPlayer : IBaseOperation, IFire
{
    public void Move()
    {
    }

    public void Jump()
    {
    }

    public void Fire()
    {
    }
    
    
}

#endregion


/// <summary>
/// 设置角色基类，实现所有接口，但可以支持内容扩展
/// </summary>
public class Role : IUseSkill,IFire,IBaseOperation
{
    public virtual void UseSkill()
    {
    }

    public virtual void Fire()
    {
    }

    public virtual void Move()
    {
    }

    public virtual void Jump()
    {
    }
}

public class GameAI : Role
{
    public override void Move()
    {
        base.Move();
    }

    public override void Fire()
    {
        base.Fire();
    }
}


public class ISPDemo : MonoBehaviour
{
    private Button btn;
    
    private void Start()
    {
        btn.onClick.AddListener(OnButtonCLick);
        btn.onClick.RemoveListener(OnButtonCLick);
        btn.onClick.AddListener(OnButtonCLick);
    }

    private void OnButtonCLick()
    {
        
    }
}