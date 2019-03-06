using UnityEngine;

/// <summary>网络管理</summary>
public class CSNetManager : CSMonoSingleton<CSNetManager>
{


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
