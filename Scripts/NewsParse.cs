using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;

[System.Serializable]
public class News
{
    public Result result;
}
[System.Serializable]
public class Result
{
    public Data[] data;
}

[System.Serializable]
public class Data
{
    public string title;
    public string date;
    public string category;
    public string author_name;
}

public class NewsParse : MonoBehaviour
{
    public TextAsset jsonText;
    public GameObject item;

    void Start()
    {
        //解析JSON数据
        News newsData = JsonUtility.FromJson<News>(jsonText.text);

        //Debug.Log(newsData.result.data[0].title);

        for(int i = 0; i < newsData.result.data.Length; i++)
        {
            GameObject curItem = Instantiate(item);
            curItem.transform.SetParent(this.transform);

            curItem.transform.GetChild(0).GetComponent<Text>().text = newsData.result.data[i].title;
            curItem.transform.GetChild(1).GetComponent<Text>().text = newsData.result.data[i].date;
            curItem.transform.GetChild(2).GetComponent<Text>().text = newsData.result.data[i].author_name;
        }
    }
}
