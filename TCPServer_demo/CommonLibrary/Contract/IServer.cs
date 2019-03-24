using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Contract
{
    /// <summary>
    /// Called by Client
    /// </summary>
    [ScsService(Version = "1.1.1")]
    public interface IServer
    {
        /// <summary>
        /// Client send data to Client
        /// Receiving string data from client
        /// </summary>
        /// <param name="data">data nhận</param>
        void OnDataReceive(string data);

        /// <summary>
        /// Client send data to Client
        /// Receiving byte array from client
        /// </summary>
        /// <param name="dataBytesArray">chuoi byte data</param>
        void OnDataReceiveBytes(byte[] dataBytesArray);

        /// <summary>
        /// Login when client first connect to Server
        /// </summary>
        /// <param name="userInfo"></param>
        void Login(UserInfo userInfo);

        /// <summary>
        /// Login when called or disconnected
        /// </summary>
        void Logout();
    }
}
