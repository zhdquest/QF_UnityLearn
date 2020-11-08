using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeInput : MonoBehaviour
{

    [Header("玩家预设体")]
    public GameObject playerPrefab;
    
    private void Update()
    {
        float hor = Input.GetAxis("Horizontal");
        
        // Debug.Log(Input.mousePosition);

        if (Input.GetButtonDown("Fire"))
        {
            Debug.Log("按住了开火键");

            // GameObject crtPlayer = Instantiate(playerPrefab);
            GameObject crtPlayer = Instantiate(playerPrefab, Vector3.forward, Quaternion.identity);
        }
    }

    void KeyUpdate()
    {
        bool downA = Input.GetKeyDown(KeyCode.A);

        if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("按下了A键");
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            Debug.Log("松开了A键");
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("按住了空格键");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("按下了鼠标左键");
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("松开了鼠标左键");
        }
        
        if (Input.GetMouseButton(0))
        {
            Debug.Log("按住了鼠标左键");
        }
    }
}
