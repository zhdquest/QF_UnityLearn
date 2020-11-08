using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadRigidbody : MonoBehaviour
{
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Debug.Log(rig.velocity);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rig.velocity = Vector3.down * 50;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rig.AddForce(10,10,0);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            rig.AddExplosionForce(500,Vector3.zero, 5);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("碰撞开始");
        Debug.Log(other.collider);
        Debug.Log(other.contacts[0].point);
        
        // other.collider.GetComponent<MeshRenderer>().material.color = Color.red;
        // rig.AddExplosionForce(100,other.contacts[0].point,5);
    }

    private void OnCollisionStay(Collision other)
    {
        Debug.Log("碰撞过程中....");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("触发开始");
        Debug.Log("Other:" + other);
        
        other.GetComponent<MeshRenderer>().material.color = Color.red;
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<MeshRenderer>().material.color = 
            new Color(Random.Range(0f,1),
            Random.Range(0f,1),
            Random.Range(0f,1));
    }
}
