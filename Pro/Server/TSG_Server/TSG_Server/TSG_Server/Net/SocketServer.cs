using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using LitJson;

namespace Net
{
    class SocketServer
    {
        private Socket socket;//当前套接字
        public Dictionary<string, SocketClient> dictionary = new Dictionary<string, SocketClient>();//string为ip地址

        public void Listen()
        {
            IPEndPoint serverIp = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(serverIp);
            socket.Listen(100);
            Console.WriteLine("server ready.");
            AsynAccept(socket);
        }

        /// <summary>
        /// 异步连接客户端
        /// </summary>
        public void AsynAccept(Socket serverSocket)
        {
            serverSocket.BeginAccept(asyncResult =>
            {
                Socket client = serverSocket.EndAccept(asyncResult);
                SocketClient socketClient = new SocketClient(client);

                string s = socketClient.GetSocket().RemoteEndPoint.ToString();
                Console.WriteLine("连接的客户端为： " + s);
                dictionary.Add(s, socketClient);

                socketClient.AsynRecive();
                socketClient.AsynSend(new SocketMessage(20, 15, "你好，客户端"));
                socketClient.AsynSend(new SocketMessage(20, 15, "你好，客户端"));
                socketClient.AsynSend(new SocketMessage(20, 15, "你好，客户端"));
                AsynAccept(serverSocket);
            }, null);
        }

        /// <summary>
        /// 解析信息
        /// </summary>
        public static void HandleMessage(SocketClient sc, SocketMessage sm)
        {
            Console.WriteLine(sc.GetSocket().RemoteEndPoint.ToString() + "   " +                sm.Length + "   " + sm.ModuleType + "   " + sm.MessageType + "   " + sm.Message);

            if (sm.ModuleType == (int)(ModuleTypeEnum.Login))
            {
                if (sm.MessageType == (int)(MessageTypeEnum.Login_Login))
                {
                    BoolDTO b;
                    LoginDTO l = SocketTool<LoginDTO>.ToObject(sm.Message);
                    if (l.Account.Equals(l.Password))
                    {
                        b = new BoolDTO(true);
                    }
                    else
                    {
                        b = new BoolDTO(false);
                    }

                    string s = SocketTool<BoolDTO>.ToJson(b);
                    sc.AsynSend(new SocketMessage((int)ModuleTypeEnum.Login, (int)MessageTypeEnum.Login_Login, s));
                }
            }
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
    
 
public class SocketTool<T>
    {

        public static string ToJson(T o)
        {
            return JsonMapper.ToJson(o);
        }

        public static T ToObject(string s)
        {
            return JsonMapper.ToObject<T>(s);
        }

    }
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
}
