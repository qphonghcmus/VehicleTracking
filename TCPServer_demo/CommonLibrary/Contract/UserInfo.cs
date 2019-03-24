using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibrary.Contract
{
    [Serializable]
    public class UserInfo
    {
        /// <summary>
        /// Account of client
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password of client
        /// </summary>
        public string Password { get; set; }

        public UserInfo(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
}
