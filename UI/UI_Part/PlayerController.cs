using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public Vector3 dir;

    public float moveSpeed = 3f;
    
    private CharacterController _characterController;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // if(dir == Vector3.zero)
        //     return;

        // transform.position += dir.normalized * moveSpeed * Time.deltaTime;
        _characterController.SimpleMove(dir * Time.deltaTime * moveSpeed);
    }
}