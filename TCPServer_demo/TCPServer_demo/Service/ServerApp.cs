using CommonLibrary.Contract;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCPServer_demo.Service
{
    public class ServerApp
    {
        /// <summary>
        /// Object is used to host on Scs Server
        /// </summary>
        private IScsServiceApplication _serverApp;

        /// <summary>
        /// Object that serves clients
        /// </summary>
        private Tracking_Server _server;

        public void Start_App()
        {
            _serverApp = ScsServiceBuilder.CreateService(new ScsTcpEndPoint(10048));
            _server = new Tracking_Server();
            _serverApp.AddService<IServer, Tracking_Server>(_server);
            _serverApp.Start();

        }
    }
}