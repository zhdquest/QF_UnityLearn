using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRay : MonoBehaviour
{
    private Ray ray;

    private RaycastHit hit;

    private void Start()
    {
        ray = new Ray(transform.position,Vector3.right);
    }

    void Update()
    {
        
        // Physics.RaycastAll()
        
        // if (Physics.Raycast(ray,out hit,10))
        // {
        //     Debug.Log("检测到了");
        //     Debug.Log(hit.collider);
        //     Debug.Log(hit.point);
        // }
        
        // Input.mousePosition

        Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(r, out hit))
        {
            Debug.Log(hit.collider);
            
            Debug.DrawLine(Camera.main.transform.position,hit.point,Color.black);
        }
    }
}
