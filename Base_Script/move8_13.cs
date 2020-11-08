using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move8_13 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public float movespeed=1;
    public float rotatespeed=10;
    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.eulerAngles += new Vector3(0,1,0) * Time.deltaTime * rotatespeed;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.eulerAngles += new Vector3(0, -1, 0) * Time.deltaTime * rotatespeed;
        }
        if (Input.GetAxis("Vertical") > 0)
        {
            transform.position+=Vector3.forward * Time.deltaTime * movespeed;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.position += -Vector3.forward * Time.deltaTime * movespeed;
        }
    }
    
}
