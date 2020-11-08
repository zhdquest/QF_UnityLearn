using System;
using UnityEngine;

public class FPSGun : MonoBehaviour
{
    [Header("声音片段")]
    public AudioClip fireClip;
    public AudioClip readyClip;
    public AudioClip reloadClip;

    [Header("火花特效网格")]
    public GameObject muzzleFlash;
    
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        //播放准备声音
        AudioSource.PlayClipAtPoint(readyClip,transform.position);
    }

    /// <summary>
    /// 开火
    /// </summary>
    public void Fire()
    {
        //将开火名称转换为哈希
        int fire = Animator.StringToHash("Fire");
        //播放开火动画
        _animator.SetTrigger(fire);
        //播放声音片段
        // AudioSource.PlayClipAtPoint(fireClip,transform.position);
    }

    /// <summary>
    ///换弹
    /// </summary>
    public void Reload()
    {
        //将换弹名称转换为哈希
        int reload = Animator.StringToHash("Reload");
        //播放换弹动画
        _animator.SetTrigger(reload);
        //播放声音片段
        // AudioSource.PlayClipAtPoint(reloadClip,transform.position);
    }

    /// <summary>
    /// 播放开火特效
    /// </summary>
    public void PlayerFireEffect()
    {
        //启动特效
        muzzleFlash.SetActive(true);
        //0.1s后关闭特效
        Invoke("UnEffect",0.1f);
    }

    /// <summary>
    /// 取消特效
    /// </summary>
    private void UnEffect()
    {
        muzzleFlash.SetActive(false);
    }
}