//using Client.Control;
using CommonLibrary.Contract;
using Force.Crc32;
using Hik.Communication.Scs.Client;
using Hik.Communication.Scs.Communication;
using Hik.Communication.Scs.Communication.EndPoints.Tcp;
using Hik.Communication.ScsServices.Client;
using Hik.Communication.ScsServices.Communication.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
       

        public static void Main(string[] args)
        {
            // a client Service to handle remote method called by server
            Tracking_Client _client = new Tracking_Client();
            // a Scs client connected to Scs Server
            var _scsClient = ScsServiceClientBuilder.CreateClient<IServer>(new ScsTcpEndPoint("127.0.0.1", 10048), _client);

            Connect(_scsClient);

            // TO DO
            //while (_scsClient.CommunicationState == CommunicationStates.Disconnected)
            //{
            //    ReConnect(_scsClient);
            //}

            // a object stored information used to login to Server
            UserInfo userInfo = new UserInfo("Phong", "123456");

            // Use UserInfo to login to Server
            Login(_scsClient, userInfo);

            // Data used to send
            string data = "This is data";

            // Send data to Server
            SendDataToServer(_scsClient, data);

            // Disconnect server
            _scsClient.Disconnect();

        }

        /// <summary>
        /// Send Data to Server
        /// use CRC-32 algorithm
        /// </summary>
        /// <param name="_scsClient"></param>
        private static void SendDataToServer(IScsServiceClient<IServer> _scsClient, string data)
        {
            // Byte array used to compute crc value
            var dataBytesArray = new Byte[data.Length];
            // Convert data to byte array
            dataBytesArray = Encoding.ASCII.GetBytes(data);
            // Entend 4 byte to store crc value
            Array.Resize(ref dataBytesArray, data.Length + 4);
            // Compute crc-32 value and write to end byte array
            Crc32Algorithm.ComputeAndWriteToEnd(dataBytesArray);

            // Send byte array to server
            _scsClient.ServiceProxy.OnDataReceiveBytes(dataBytesArray);
        }

        private static void Login(IScsServiceClient<IServer> _scsClient, UserInfo userInfo)
        {
            try
            {
                // Login to Server
                _scsClient.ServiceProxy.Login(userInfo);
            }
            catch (ScsRemoteException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void ReConnect(IScsServiceClient<IServer> _scsClient)
        {
            try
            {
                var reConnecter = new ClientReConnecter(_scsClient) { ReConnectCheckPeriod = 30000 };
                
            }
            catch { }
        }

        private static bool Connect(IScsServiceClient<IServer> _scsClient)
        {
            if (_scsClient.CommunicationState == CommunicationStates.Connected)
            {
                try
                {
                    _scsClient.Disconnect();
                }
                catch
                {

                }
            }
            // Connect to Server
            try
            {
                _scsClient.Connect();
            }
            catch { }

            if (_scsClient.CommunicationState == CommunicationStates.Connected)
            {
                return true;
            }
            return false;
        }
    }
}
