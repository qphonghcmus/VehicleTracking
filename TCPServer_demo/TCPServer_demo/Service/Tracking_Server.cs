using CommonLibrary.Contract;
using Force.Crc32;
using Hik.Collections;
using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;


namespace TCPServer_demo.Service
{
    /// <summary>
    /// Implement IServer
    /// </summary>
    public class Tracking_Server : ScsService, IServer
    {
        #region Private Fields
        /// <summary>
        /// List of all clients connected
        /// </summary>
        private readonly ThreadSafeSortedList<long, SubClient> _clients;
        #endregion

        #region Constructor
        public Tracking_Server()
        {
            _clients = new ThreadSafeSortedList<long, SubClient>();
        }
        #endregion

        #region public Properties
        /// <summary>
        /// Get a list of clients
        /// </summary>
        public List<UserInfo> UserList
        {
            get
            {
                return (from client in _clients.GetAllItems() select client.User).ToList();
            }
        }
        #endregion


        #region Methods
        /// <summary>
        /// Login when client first connect to Server
        /// </summary>
        /// <param name="userInfo"></param>
        public void Login(UserInfo userInfo)
        {
            // get a reference to current client calling method
            var clientRef = CurrentClient;

            //Get a proxy object to call methods of client 
            var clientProxy = clientRef.GetClientProxy<IClient>();

            // Create a SubClient and store in _clients
            var clientObj = new SubClient(clientRef, clientProxy, userInfo);
            _clients[clientRef.ClientId] = clientObj;

            // Registered to Disconnected event to know when user connect is closed
            clientRef.Disconnected += ClientRef_Disconnected;
        }

        /// <summary>
        /// Disconneted event of client
        /// </summary>
        /// <param name="sender">client object that is disconnected</param>
        /// <param name="e"></param>
        private void ClientRef_Disconnected(object sender, EventArgs e)
        {
            // get client object
            var client = (IScsServiceClient)sender;

            // logout when disconnected
            ClientLogout(client.ClientId);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Logout()
        {
            ClientLogout(CurrentClient.ClientId);
        }

        /// <summary>
        /// Call when a client call logout method or client disconnected
        /// </summary>
        /// <param name="clientId">id of client that call method or disconnected</param>
        public void ClientLogout(long clientId)
        {
            // Get client from the list
            var client = _clients[clientId];
            if (client == null)
            {
                return;
            }

            // Remove client from list
            _clients.Remove(client.Client.ClientId);

        }

        /// <summary>
        /// Receiving string data from client
        /// </summary>
        /// <param name="data"></param>
        public void OnDataReceive(string data)
        {
            // Get client object from list
            var client = _clients[CurrentClient.ClientId];
            // Response to client
            client.ClientProxy.OnResponseReceive("OK!");
        }

        /// <summary>
        /// Receiving byte array from client
        /// </summary>
        /// <param name="dataBytesArray"></param>
        public void OnDataReceiveBytes(byte[] dataBytesArray)
        {
            // Get client object from list
            var client = _clients[CurrentClient.ClientId];

            // Check Crc 
            if (Crc32Algorithm.IsValidWithCrcAtEnd(dataBytesArray))
            {
                // Remove last 4 bytes that is crc value
                Array.Resize(ref dataBytesArray, dataBytesArray.Length - 4);
                // Convert to string 
                string data = Encoding.ASCII.GetString(dataBytesArray);

                // Reponse to client
                client.ClientProxy.OnResponseReceive("OK!");
            }
            else
            {
                client.ClientProxy.OnResponseReceive("Error");
            }
        }



        #endregion

        #region Sub Class to store information for a connected client
        /// <summary>
        /// Sub Class is used to store information for a connected client
        /// </summary>
        private class SubClient
        {
            /// <summary>
            /// Scs client reference.
            /// </summary>
            public IScsServiceClient Client { get; private set; }

            /// <summary>
            /// Proxy object to call remote methods of chat client.
            /// </summary>
            public IClient ClientProxy { get; private set; }

            /// <summary>
            /// User informations of client.
            /// </summary>
            public UserInfo User { get; private set; }

            /// <summary>
            /// Creates a new ChatClient object.
            /// </summary>
            /// <param name="client">Scs client reference</param>
            /// <param name="clientProxy">Proxy object to call remote methods of chat client</param>
            public SubClient(IScsServiceClient client, IClient clientProxy, UserInfo userInfo)
            {
                Client = client;
                ClientProxy = clientProxy;
                User = userInfo;
            }
        }
        #endregion
    
    }
}