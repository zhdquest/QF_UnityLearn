using System;
using UnityEngine;

public class SocketDemo : MonoBehaviour
{
    public GUISkin skin;
    
    private string serverIP = "127.0.0.1";
    private string serverPort = "12345";
    //消息列表
    private string log = "";
    //要发送的消息
    private string sendMsg = "";

    private int currentState = 0;

    private void Awake()
    {
        Application.runInBackground = true;
    }

    private void OnGUI()
    {
        //设置皮肤
        GUI.skin = skin;
        
        //【未联网状态】
        if (currentState == 0)
        {
            GUILayout.Label("请输入服务端IP:");
            serverIP = GUILayout.TextField(serverIP);
            GUILayout.Label("请输入服务端Port:");
            serverPort = GUILayout.TextField(serverPort);

            if (GUILayout.Button("创建服务器"))
            {
                //创建服务器，服务器收到的回调消息，传输给Log去显示
                SocketTool.GetInstance().ServerInit(msg =>
                {
                    log += msg + "\n";
                });
                //改变状态为【服务器状态】
                currentState = 1;
            }

            if (GUILayout.Button("连接服务器"))
            {
                SocketTool.GetInstance().ClientConnect(
                    serverIP,int.Parse(serverPort),
                    msg =>
                    {
                        log += msg + "\n";
                    });
                //改变状态为【客户端状态】
                currentState = 2;
            }
        }
        //【服务器状态】
        else if(currentState == 1)
        {
            //暂无操作
        }
        //【客户端状态】
        else if (currentState == 2)
        {
            GUILayout.Label("请输入要发送的消息：");
            sendMsg = GUILayout.TextField(sendMsg);

            if (GUILayout.Button("发送..."))
            {
                //客户端发送消息
                SocketTool.GetInstance().ClientSend(sendMsg);
            }
        }

        //显示日志
        GUILayout.Label(log);
    }

    private void OnApplicationQuit()
    {
        //收尾
        SocketTool.GetInstance().SocketDispose();
    }
}