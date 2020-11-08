using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class fire8_13 : MonoBehaviour
{
    public GameObject bullet;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject b=Instantiate(bullet, transform.position, transform.rotation); 
            Destroy(b, 2);
            //Color a = new Color(Random.Range(1, 4));
        }
        Material a= this.transform.GetComponent<MeshRenderer>().material;
        //a.color=new Color()
        //Physics.Raycast();
        //Debug.DrawLine
    }
    
}
