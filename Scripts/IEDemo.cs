using System;
using System.Collections;
using UnityEngine;

public class IEDemo : MonoBehaviour
{
    private IEnumerator gw;

    private Coroutine _coroutine;
    
    private IEnumerator Start()
    {
        //启动协程
        // StartCoroutine(Demo());

        gw = Demo();
         
        // _coroutine = StartCoroutine(gw);

        StartCoroutine("GenerateWave");
        
        yield break;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("GenerateWave");
            StopCoroutine("GenerateMonsters");
        }
    }

    IEnumerator Demo()
    {
        while (false)
        {
            Debug.Log("1");
        
            // yield return null;//等一帧
            yield return new WaitForEndOfFrame();//等一帧
        }

        while (false)
        {
            Debug.Log("1");
            yield return new WaitForSeconds(2);
        }

        while (true)
        {
            Debug.Log("1");
            
            yield return new WaitForFixedUpdate();
        }
    }

    IEnumerator GenerateWave()
    {
        while (true)
        {
            //生成当前波次的怪物
            yield return StartCoroutine("GenerateMonsters");
            
            Debug.Log("当前波次怪物生成完毕！");
            
            yield return new WaitForSeconds(3);
        }
    }

    IEnumerator GenerateMonsters()
    {
        int count = 0;
        
        while (true)
        {
            if (count == 3)
                yield break;
            
            Debug.Log("生成一个怪物 : " + count);
            count++;
            yield return new WaitForSeconds(1);
        }
    }
}