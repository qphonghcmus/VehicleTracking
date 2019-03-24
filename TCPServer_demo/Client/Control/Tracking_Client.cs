using CommonLibrary.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Tracking_Client : IClient
    {
        /// <summary>
        /// Server send response to Client
        /// Client receive response from server
        /// </summary>
        /// <param name="res"></param>
        public void OnResponseReceive(string res)
        {
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
