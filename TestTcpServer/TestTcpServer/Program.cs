using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a server that listen at TCP port 10077
            var server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(10077));

            // Register events of server when client connect or disconnect
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            // Start server
            server.Start();

            Console.WriteLine("Server is started");

            // Press enter to stop server
            Console.ReadLine();
            server.Stop();
        }

        private static void Server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            Console.WriteLine("A Client is disconnected");
        }

        private static void Server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            Console.WriteLine("A new Client is connected");
            // Register event when client send message to server
            e.Client.MessageReceived += Client_MessageReceived;
        }

        // Khi gửi data lên server thì event này không được kích hoạt mà client bị disconnect và nhảy tới sự kiện Disconnect ở trên
        // 
        private static void Client_MessageReceived(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
        {
            var message = e.Message as ScsTextMessage;
            if(message == null)
            {
                return;
            }

            // Get a reference to client
            var client = (IScsServerClient)sender;
            Console.WriteLine("Client send message: " + message.Text);

            // Reply to client
            client.SendMessage(new ScsTextMessage("I got your message"));
        }
    }
}
