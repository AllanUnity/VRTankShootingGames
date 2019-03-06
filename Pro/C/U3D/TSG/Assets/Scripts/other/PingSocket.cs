using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine;

public class PingSocket : MonoBehaviour
{
    List<string> ipList = new List<string>();

    public void DoGetIP()
    {
        //Thread
        StartCoroutine(getIP());
    }

    IEnumerator getIP()
    {
        yield return new WaitForSeconds(1f);

        //获取本地机器名 
        string _myHostName = Dns.GetHostName();
        //获取本机IP 
        string _myHostIP = Dns.GetHostEntry(_myHostName).AddressList[0].ToString();

        //截取IP网段
        string ipDuan = _myHostIP.Remove(_myHostIP.LastIndexOf('.'));
        //string ipDuan = "192.168.0";
        //枚举网段计算机
        for (int i = 1; i <= 255; i++)
        {
            System.Net.NetworkInformation.Ping myPing = new System.Net.NetworkInformation.Ping();
            myPing.PingCompleted += new PingCompletedEventHandler(_myPing_PingCompleted);
            string pingIP = ipDuan + "." + i.ToString();
            myPing.SendAsync(pingIP, 500, null);

        }
    }
    void _myPing_PingCompleted(object sender, PingCompletedEventArgs e)
    {
        if (e.Reply.Status == IPStatus.Success)
        {
            ipList.Add(e.Reply.Address.ToString());
        }

        Debug.LogError("局域网主机---" + e.Reply.Address.ToString());
    }
}
