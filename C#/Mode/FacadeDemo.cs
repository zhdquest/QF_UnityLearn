using System;
using UnityEngine;
using Utilty;

public class FacadeDemo : MonoBehaviour {
    
    private void Start()
    {
        PrefabManager.GetInstance().CreateGameObjectByPrefab("RedPanel",
            transform, Vector3.zero, Quaternion.identity);
    }
}