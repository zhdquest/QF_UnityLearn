using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonController : MonoBehaviour
{

    private Button btn;


    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(OnButtonClick);
    }


    public void OnButtonClick()
    {
        //Debug.Log("Click!" + a);
    }
}