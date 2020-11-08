using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Loading : MonoBehaviour
{
    public Vector2 speedRange = new Vector2(-1,2);
    
    private Scrollbar loadingScrollbar;
    private AsyncOperation asyncOperation;

    private void Awake()
    {
        loadingScrollbar = GetComponent<Scrollbar>();
    }

    private void Start()
    {
        //异步加载场景
        asyncOperation = SceneManager.LoadSceneAsync("Game");
        asyncOperation.allowSceneActivation = false;
        // Debug.Log(asyncOperation.allowSceneActivation);
    }
    
    private void Update()
    {
        if (loadingScrollbar.size == 1)
        {
            asyncOperation.allowSceneActivation = true;
        }

        //真的
        // loadingScrollbar.size = asyncOperation.progress * 10/9;
        
        //假的
        loadingScrollbar.size += Time.deltaTime * Random.Range(speedRange.x,speedRange.y);
    }
}