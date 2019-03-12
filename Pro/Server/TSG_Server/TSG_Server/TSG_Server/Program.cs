using Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TSG_Server
{
    class Program
    {

        static void Main(string[] args)
        {
            SocketServer socketServer = new SocketServer();
            socketServer.Listen();
            Console.ReadKey();
        }

    }
}
