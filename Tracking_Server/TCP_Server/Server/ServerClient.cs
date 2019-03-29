using Hik.Communication.Scs.Communication.Messages;
using Hik.Communication.Scs.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TCP_Server.Server
{
    /// <summary>
    /// Object represents a client that connect to server
    /// </summary>
    public class ServerClient : IDisposable
    {
        #region properties

        private readonly IScsServerClient _client;

        #endregion

        #region Constructor

        public ServerClient(IScsServerClient client)
        {
            _client = client;

            // Registered event when client send message to server and client disconected
            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Client_Disconnected;
        }

        #endregion

        #region Private methods

        private void Client_Disconnected(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void Client_MessageReceived(object sender, Hik.Communication.Scs.Communication.Messages.MessageEventArgs e)
        {
            try
            {
                // Message used to send or received in StreamWireProtol is ScsRawDataMessage
                var msg = e.Message as ScsRawDataMessage;
                if (msg == null) return;

                // Convert byte array to string
                var data = Encoding.ASCII.GetString(msg.MessageData);

                // Response to Remote App
                var rep = "\r\nOK!\r\n";
                _client.SendMessage(new ScsRawDataMessage(Encoding.ASCII.GetBytes(rep)));

            }catch(Exception )
            {

            }
        }

        #endregion

        #region Implement IDisposable

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}