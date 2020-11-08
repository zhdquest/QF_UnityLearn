using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketTool
{
    #region Singleton
    private static SocketTool instance;
    public static SocketTool GetInstance()
    {
        if (instance == null)
        {
            instance = new SocketTool();
        }
        return instance;
    }
    private SocketTool()
    {
    }
    #endregion

    #region Server

    //服务端套接字
    private Socket serverSocket;
    //服务端上层回调
    private Action<string> serverCallback;
    //已经连接的客户端
    private Dictionary<IPAddress, Socket> connectedClient;
    //服务端消息缓存
    private byte[] serverBuffer;

    /// <summary>
    /// 初始化服务端
    /// </summary>
    public void ServerInit(Action<string> serverCallback)
    {
        //设置委托回调
        this.serverCallback = serverCallback;
        //实例化字典
        connectedClient = new Dictionary<IPAddress, Socket>();
        //实例化缓存长度
        serverBuffer = new byte[1024];
        //实例化Socket对象【TCP】
        serverSocket = new Socket(
            AddressFamily.InterNetwork,/*地址族：IPV4*/
            SocketType.Stream,/*双向读写流*/
            ProtocolType.Tcp/*传输层协议类型*/);
        //实例化Socket对象【UDP】
        // serverSocket = new Socket(
        //     AddressFamily.InterNetwork,/*地址族：IPV4*/
        //     SocketType.Dgram,/*数据报*/
        //     ProtocolType.Udp/*传输层协议类型*/);

        serverCallback("服务端创建完毕...");
        //创建IP网络结点
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any,12345);
        //绑定网络结点
        serverSocket.Bind(ipEndPoint);
        serverCallback("网络结点绑定完毕...");
        //设置监听队列
        serverSocket.Listen(5);
        //异步接受客户端的连接请求
        serverSocket.BeginAccept(ServerAccept, serverSocket);
        serverCallback("异步接受请求开始...");
    }

    /// <summary>
    /// 异步接受请求后，接受到客户端的回调
    /// </summary>
    /// <param name="ar"></param>
    private void ServerAccept(IAsyncResult ar)
    {
        //获取到异步接受的监听
        Socket listener = ar.AsyncState as Socket;
        //结束异步接受
        Socket acceptClient = listener.EndAccept(ar);
        //获取该客户端的本地地址
        IPAddress clientLocalAddress = (acceptClient.LocalEndPoint 
            as IPEndPoint).Address;
        //将当前客户端的Socket保存
        connectedClient.Add(clientLocalAddress,acceptClient);
        serverCallback("异步接受请求结束..." + acceptClient.LocalEndPoint);
        //开始对该客户端进行异步的消息接收
        acceptClient.BeginReceive(serverBuffer, /*消息缓存*/
            0, /*消息接收的偏移量*/
            serverBuffer.Length, /*消息接收长度*/
            SocketFlags.None, /*消息接收的特殊标记*/
            ServerReceive, /*消息接受到后的回调*/
            acceptClient/*状态信息*/);
        serverCallback("异步消息接收开始...");
        //尾递归【继续接受其他客户端的连接请求】
        serverSocket.BeginAccept(ServerAccept, serverSocket);
    }

    /// <summary>
    /// 异步接收消息的回调
    /// </summary>
    /// <param name="ar"></param>
    private void ServerReceive(IAsyncResult ar)
    {
        //获取异步状态
        Socket listener = ar.AsyncState as Socket;
        //收到消息的字节数
        int count = listener.EndReceive(ar);
        serverCallback("异步消息接收结束，接收了..." + count + "个字节...");
        //将收到的字节流转换成字符串
        string msg = UTF8Encoding.UTF8.GetString(serverBuffer);
        serverCallback("接收到的消息为..." + msg);
        //清空字节流
        serverBuffer = new byte[1024];
        //尾递归
        listener.BeginReceive(serverBuffer, /*消息缓存*/
            0, /*消息接收的偏移量*/
            serverBuffer.Length, /*消息接收长度*/
            SocketFlags.None, /*消息接收的特殊标记*/
            ServerReceive, /*消息接受到后的回调*/
            listener/*状态信息*/);
    }

    #endregion

    #region Client

    //客户端套接字
    private Socket clientSocket;
    //客户端缓存
    private byte[] clientBuffer;
    //客户端消息回调
    private Action<string> clientCallback;

    /// <summary>
    /// 客户端连接
    /// </summary>
    public void ClientConnect(string serverIP,
        int serverPort,Action<string> clientCallback)
    {
        //设置回调
        this.clientCallback = clientCallback;
        //实例化Socket
        clientSocket = new Socket(
            AddressFamily.InterNetwork,
            SocketType.Stream,ProtocolType.Tcp);

        clientCallback("客户端创建完毕..");
        //组装网络结点
        IPEndPoint ipEndPoint = new IPEndPoint(
            IPAddress.Parse(serverIP),serverPort);
        //连接服务器
        clientSocket.BeginConnect(ipEndPoint, ClientConnected, clientSocket);
        clientCallback("客户端开始异步连接服务器..");
    }

    /// <summary>
    /// 客户端连接成功
    /// </summary>
    /// <param name="ar"></param>
    private void ClientConnected(IAsyncResult ar)
    {
        Socket listener = ar.AsyncState as Socket;
        //结束连接
        listener.EndConnect(ar);
        clientCallback("客户端连接服务器成功!..");
    }

    /// <summary>
    /// 客户端发送消息
    /// </summary>
    /// <param name="msg"></param>
    public void ClientSend(string msg)
    {
        //将消息转换为字节数组，保存到buffer
        clientBuffer = UTF8Encoding.UTF8.GetBytes(msg);
        //发消息
        clientSocket.BeginSend(clientBuffer, 0,
            clientBuffer.Length, SocketFlags.None,
            ClientSendSuccess,clientSocket);
        clientCallback("客户端向服务端发送消息.." + msg);
    }

    private void ClientSendSuccess(IAsyncResult ar)
    {
        Socket listener = ar.AsyncState as Socket;
        //结束发送
        int count = listener.EndSend(ar);
        clientCallback("客户端成功发送消息" + count + "个字节...");
    }

    #endregion

    #region Socket Dispose

    /// <summary>
    /// 收尾操作
    /// </summary>
    public void SocketDispose()
    {
        if (serverSocket != null)
        {
            serverSocket.Close();
            serverSocket = null;
        }

        if (clientSocket != null)
        {
            clientSocket.Disconnect(true);
            clientSocket.Close();
            clientSocket = null;
        }
    }

    #endregion

}