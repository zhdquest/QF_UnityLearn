using UnityEngine;

#region LSP

public abstract class Gun
{
    public abstract void Fire();

    public virtual void Reload()
    {
        
    }
}

/// <summary>
/// 有开镜功能的枪支
/// </summary>
public class OpenSightGun
{
    /// <summary>
    /// 开启瞄准镜
    /// </summary>
    public virtual void OpenSight()
    {
        
    }
}

/// <summary>
/// 手枪
/// </summary>
public class Pistol : Gun
{
    public override void Fire()
    {
        
    }

    public override void Reload()
    {
        
    }
}

#endregion





public class LSPDemo : MonoBehaviour {
    
    
    
}