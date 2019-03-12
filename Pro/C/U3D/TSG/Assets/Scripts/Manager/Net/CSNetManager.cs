using Net;
using UnityEngine;

/// <summary>网络管理</summary>
public class CSNetManager : CSMonoSingleton<CSNetManager>
{
    public SocketClient socketClient;
    public delegate void SendDelegate(SocketMessage sm);
    public event SendDelegate sendEvent = null;

    public override void Init()
    {
        base.Init();

        return;
        sendEvent += PrintInfor;
        socketClient = new SocketClient();
        socketClient.AsynConnect();
    }

    public void Send(SocketMessage sm)
    {
        sendEvent(sm);
    }
    private void OnDestroy()
    {
        if (socketClient != null)
        {
            socketClient.Destroy();
        }
    }
    public void PrintInfor(SocketMessage sm)
    {
        Debug.Log("消息长度:" + sm.Length + "MType:" + sm.ModuleType + "MessageType:" + sm.MessageType + "Message:" + sm.Message);
    }
    /// <summary>当前网络状态</summary>
    public string GetNetwork()
    {
        string network = string.Empty;
        switch (Application.internetReachability)
        {
            case NetworkReachability.NotReachable:
                network = "当前网络不可用";
                break;
            case NetworkReachability.ReachableViaCarrierDataNetwork:
                network = "当前网络:3G/4G";
                break;
            case NetworkReachability.ReachableViaLocalAreaNetwork:
                network = "当前网络:WIFI";
                break;
            default:
                break;
        }
        return network;
    }
}
public enum ModuleTypeEnum
{
    Login,
    CharacterControl,
}
public enum MessageTypeEnum
{
    Login_Login,
    Login_Register,

    Character,
}


[SerializeField]
public class LoginDTO
{

    public string Account { get; set; }
    public string Password { get; set; }

    public LoginDTO()
    {
    }

    public LoginDTO(string account, string password)
    {
        Account = account;
        Password = password;
    }

}
[SerializeField]
public class BoolDTO
{

    public bool Value { get; set; }

    public BoolDTO()
    {
    }

    public BoolDTO(bool value)
    {
        Value = value;
    }
}