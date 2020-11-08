using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WWWDemo : MonoBehaviour
{
    public Image _image;
    
    private string appKey = "bd6d102ed7d4cdbe209c05d413eedcd3";
    private string newsURL = "http://v.juhe.cn/toutiao/index?type=top&key=";

    private string textureURL =
        "https://ss0.bdstatic.com/94oJfD_bAAcT8t7mm9GUKT-xh_/timg?image&quality=100&size=b4000_4000&sec=1599190502&di=06fcf8d4c6a09e7c3b672cf7c1176e58&src=http://c.hiphotos.baidu.com/zhidao/pic/item/f3d3572c11dfa9ecc8a8866860d0f703908fc1cf.jpg";
    
    private IEnumerator Start()
    {
        // yield return StartCoroutine(DownloadText());

        // yield return StartCoroutine(DownloadTexture());

        yield return StartCoroutine(PostDown());
    }

    IEnumerator PostDown()
    {
        WWWForm typeForm = new WWWForm();
        
        typeForm.AddField("key",appKey);
        typeForm.AddField("type","keji");
        
        //创建WWW对象
        WWW www = new WWW("http://v.juhe.cn/toutiao/index",typeForm);

        yield return www;
        
        Debug.Log(www.text);

    }

    IEnumerator DownloadTexture()
    {
        WWW www = new WWW(textureURL);

        while (!www.isDone)
        {
            //下载进度
            Debug.Log(www.progress); 
            yield return 0;
        }
        
        Texture2D texture2D = www.texture;
        
        //将图片贴到圆柱体身上
        GetComponent<MeshRenderer>().material.mainTexture = texture2D;
        
        //将下好的图片转换成精灵，赋给Image
        _image.sprite = Sprite.Create(texture2D,
            new Rect(0,0,texture2D.width,texture2D.height),
            new Vector2(0,0));
    }

    IEnumerator DownloadText()
    {
        WWW www = new WWW(newsURL + appKey);
        //等待下载完毕后恢复
        yield return www;
        Debug.Log(www.text);
    }

    IEnumerator DownloadAudio()
    {
        WWW www = new WWW("http://www.baidu.com/abc.mp3");

        yield return www;
        //获取声音
        AudioClip clip = www.GetAudioClip();
        //播放声音
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    IEnumerator DownloadMovieTexture()
    {
        WWW www = new WWW("http://www.baidu.com/abc.ogg");

        yield return www;

        //视频
        //MovieTexture movieTexture = www.GetMovieTexture();

        //声音片段
        //AudioClip clip = movieTexture.audioClip;
    }
}