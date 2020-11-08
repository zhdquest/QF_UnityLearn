using System;
using System.IO;
using UnityEngine;
using System.Collections;
using UnityEngine.Video;

public class DownloadVideo : MonoBehaviour
{
    private string url =
        "http://data.vod.itc.cn/?rb=1&key=jbZhEJhlqlUN-Wj_HEI8BjaVqKNFvDrn&prod=flash&pt=1&new=/51/116/UdKGIuSjQIO8dynrybyS1E.mp4";

    private VideoPlayer _videoPlayer;
    
    private IEnumerator Start()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
        //加载视频
        VideoClip videoClip = Resources.Load<VideoClip>("Videos/demo");
        //判断是否加载成功
        if (videoClip == null)
        {
            yield return StartCoroutine(DownloadMovie());
            videoClip = Resources.Load<VideoClip>("Videos/demo");
        }
        //设置Clip
        _videoPlayer.clip = videoClip;
        //播放
        _videoPlayer.Play();
    }

    private IEnumerator DownloadMovie()
    {
        WWW www = new WWW(url);

        while (!www.isDone)
        {
            Debug.Log("Progress: " + www.progress);
            yield return 0;
        }

        string path = Application.dataPath + "/Resources/Videos/" + "demo.mp4";
        
        File.WriteAllBytes(path,www.bytes);
    }
}