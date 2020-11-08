using UnityEngine;
using UIFrame;

public class Test : MonoBehaviour {
    private void Start()
    {
        UIManager.GetInstance().PushModule("MainPanel");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UIManager.GetInstance().PushModule("TaskPanel");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UIManager.GetInstance().PushModule("PlayerPanel");
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // UIManager.GetInstance().PushModule("MessagePanel");
            UIManager.GetInstance().OpenModule("MessagePanel",Vector2.right * 650);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UIManager.GetInstance().CloseModule("MessagePanel");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.GetInstance().PopModule();
        }
    }
}
