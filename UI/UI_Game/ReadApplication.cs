using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ReadApplication : MonoBehaviour {
    
    
    public Image _image;
    public RawImage _RawImage;
    
    private void Update()
    {
        //支持后台运行
        // Application.runInBackground = true;
        // //表示当前工程下的Assets文件夹【开发者测试】
        // Debug.Log("Data Path:" + Application.dataPath);
        // //流文件路径
        // Debug.Log("Streaming Path:" + Application.streamingAssetsPath);
        // //持久化路径
        // Debug.Log("Persistent Path:" + Application.persistentDataPath);
        
        if(!Input.GetKeyDown(KeyCode.Space))
            return;
        
        // ScreenCapture.CaptureScreenshot("Demo");
        Texture2D texture2D = ScreenCapture.CaptureScreenshotAsTexture(4);
        _RawImage.gameObject.SetActive(true);
        _RawImage.texture = texture2D;
        
        //纹理如何转换成精灵
        Sprite sprite = Sprite.Create(texture2D, new Rect(0, 0, texture2D.width, texture2D.height),
            Vector2.zero);
        _image.gameObject.SetActive(true);
        //设置精灵
        _image.sprite = sprite;
        
        // Application.OpenURL("http://www.baidu.com");
    }
}