using System;
using UnityEngine;
using UnityEngine.UI;

public class ReadText : MonoBehaviour
{
    [Header("渐变颜色")] 
    public string color = "red";
    [Header("渐变时间间隔")]
    public float interval = 0.3f;
    [Header("打字机动画开关")]
    public bool animationPlayer = false;
    
    private string head;
    private string end;

    private Text m_Text;

    private float timer;

    private int index = 0;
    //原始文本
    private string originText;

    
    
    private void Awake()
    {
        m_Text = GetComponent<Text>();
    }

    private void Start()
    {
        head = "<color=" + color + ">";
        end = "</color>";
        originText = m_Text.text;
    }

    private void Update()
    {
        if (animationPlayer)
        {
            timer += Time.deltaTime;
            if (timer > interval)
            {
                //What's your name?
                
                //a = abc
                //b = a.insert(0,111)
                //a = abc
                //b = 111abc

                if (index + 1 >= originText.Length)
                {
                    //先插入尾标签
                    m_Text.text = originText.Insert(index + 1, end);
                    //再插入头标签
                    m_Text.text = m_Text.text.Insert(0, head);
                    
                    return;
                }

                //检测到空格直接跳过
                while (originText[index + 1] == ' ')
                {
                    index++;
                }
                //先插入尾标签
                m_Text.text = originText.Insert(++index, end);
                //再插入头标签
                m_Text.text = m_Text.text.Insert(0, head);
                
                //计时器归零
                timer = 0;
                
            }
        }
    }
}