using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TCP_Server.Server
{
    public class TcpServer : IDisposable
    {

        #region properties

        public IScsServer _server;

        #endregion

        #region public methods

        /// <summary>
        /// Create TCP server by ScsServerFactory in Scs
        /// </summary>
        /// <param name="ip">ip address</param>
        /// <param name="port">port number</param>
        public void CreateServer(string ip, int port)
        {
            _server = ScsServerFactory.CreateServer(new ScsTcpEndPoint(ip, port));

            // Registered events for server when client connected and disconnected
            _server.ClientConnected += _server_ClientConnected;
            _server.ClientDisconnected += _server_ClientDisconnected;
        }


        public void StartServer()
        {
            _server.Start();
        }

        public void StopServer()
        {
            _server.Stop();
        }

        #endregion

        #region private methods

        private void _server_ClientDisconnected(object sender, ServerClientEventArgs e)
        {
            
        }

        private void _server_ClientConnected(object sender, ServerClientEventArgs e)
        {
            // set a wire protocol that is used while reading and writing message
            e.Client.WireProtocol = new StreamWireProtocol();

            // create a object that represent client to handle events
            var client = new ServerClient(e.Client);
        }

        #endregion




        #region implement IDisposable
        public void Dispose()
        {
            //throw new NotImplementedException();
        }
        #endregion
    }
}