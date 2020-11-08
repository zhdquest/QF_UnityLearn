using System;
using UnityEngine;

public class HPMPController : MonoBehaviour
{
    [Header("血条")]
    public RectTransform hpTr;
    [Header("蓝条")]
    public RectTransform mpTr;
    [Header("头像")]
    public RectTransform header;

    public float speed = 3f;
    
    private float hor, ver;

    private float maxHP_w, maxMP_w;

    private void Start()
    {
        maxHP_w = hpTr.rect.width;
        maxMP_w = mpTr.rect.width;
        
        Debug.Log(header.anchoredPosition);
        
        header.anchoredPosition = new Vector2(200,-200);
    }

    private void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        hpTr.sizeDelta = new Vector2(hpTr.sizeDelta.x + hor * Time.deltaTime * speed,hpTr.sizeDelta.y);
        mpTr.sizeDelta = new Vector2(mpTr.sizeDelta.x + ver * Time.deltaTime * speed,mpTr.sizeDelta.y);
        
        hpTr.sizeDelta = new Vector2(Mathf.Clamp(hpTr.sizeDelta.x,0,maxHP_w),hpTr.sizeDelta.y);
        mpTr.sizeDelta = new Vector2(Mathf.Clamp(mpTr.sizeDelta.x,0,maxMP_w),mpTr.sizeDelta.y);
        
    }
}