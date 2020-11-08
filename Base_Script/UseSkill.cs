using System;
using UnityEngine;
using UnityEngine.UI;


public class UseSkill : MonoBehaviour
{
    [Header("技能冷却时间")]
    public float cd = 3f;
    
    private Image m_image;

    private Text m_cdText;

    //开始转CD
    private bool beginCD = false;

    private float timer;

    private void Awake()
    {
        m_image = GetComponent<Image>();
        m_cdText = transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !beginCD)
        {
            //设置图片填充值为1
            m_image.fillAmount = 1;
            
            
            //开始转CD
            beginCD = true;
            //设置冷却时间
            timer = cd;
        }

        //如果已经开始转CD
        if (beginCD)
        {
            //填充值减去当前帧的一部分
            m_image.fillAmount -= Time.deltaTime/cd;
            //剩余时间
            timer -= Time.deltaTime;
            //文本显示[保留一位小数]
            m_cdText.text = timer.ToString("0.0");
            
            //CD转完了
            if (m_image.fillAmount < 0.01f)
            {
                m_image.fillAmount = 0;
                m_cdText.text = "";
                beginCD = false;
            }
        }
    }
}