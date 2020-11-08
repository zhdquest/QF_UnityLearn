using System;
using UnityEngine;

namespace AAA
{
    namespace ABC
    {
        
    }
    
}

public class StudentScoreOutOfRangeException : Exception
{
    public StudentScoreOutOfRangeException(string msg) : base(msg)
    {
        Debug.Log("出现了学生分数异常....");
    }
}

public class TryCatchDemo : MonoBehaviour
{
    private GameObject prefab;
    
    int number = 0;

    public float scoreSum = 680;

    public float mathScore = 130;
    
    private void Start()
    {
        prefab = new GameObject("OBJ");

        try
        {
            // prefab.name = (1 / number).ToString();

            int[] array = new[] {1, 2, 3};

            print(array[10]);

        }
        catch (NullReferenceException e)
        {
            Debug.Log(e.Message);
            Debug.Log("预设体不存在，请设置预设体后再试。。。");
        }
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);
        }
        catch (DivideByZeroException e)
        {
            Debug.Log("除数为0");
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.Log("越界了");
        }
        catch (Exception ex)
        {
            
        }
        finally
        {
            Debug.Log("123");
        }

        try
        {
            AddScore(mathScore);
        }
        catch (StudentScoreOutOfRangeException e)
        {
            
        }
    }

    public void AddScore(float score)
    {
        float newScore = scoreSum + score;

        if (newScore > 750 || newScore < 0)
        {
            throw new StudentScoreOutOfRangeException("高考分数超出了范围！");
        }
        else
        {
            scoreSum = newScore;
        }
    }
}