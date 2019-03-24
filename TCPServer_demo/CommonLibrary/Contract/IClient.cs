using Hik.Communication.ScsServices.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Contract
{
    /// <summary>
    /// Called by Server
    /// </summary>
    [ScsService(Version = "1.1.1")]
    public interface IClient
    {
        /// <summary>
        /// Send Response to Client
        /// Client receive response from server
        /// </summary>
        /// <param name="res">response</param>
        void OnResponseReceive(string res);
    }
}
