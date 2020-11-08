using UnityEngine;
using System.Diagnostics;
using Debug = UnityEngine.Debug;

public class ContitionDemo
{
    [Conditional("ABC")]
    public static void ShowMe()
    {
        Debug.Log("Show Me");
    }
}

public class HasConditionDemo : MonoBehaviour {
    private void Start()
    {
#if AAA
        
        Debug.Log("Secret");
        
#endif
    }
}