using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Server
{
    public class TCP_Server_Program
    {
        private static TcpServer _server;
        private static string IpAddress = "127.0.0.1";
        private static int PortNumber = 10084;

        public static void Main()
        {
            _server = new TcpServer();
            _server.CreateServer(IpAddress, PortNumber);
            _server.StartServer();
        }
    }
}