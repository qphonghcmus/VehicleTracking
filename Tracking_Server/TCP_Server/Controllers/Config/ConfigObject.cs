using ConfigFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace TCP_Server.Controllers.Config
{
    public class ConfigObject : IConfigObject
    {
        #region properties

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public int Port { get; set; }

        #endregion

        public void Fix()
        {
            if (string.IsNullOrEmpty(IpAddress))
            {
                IpAddress = "127.0.0.1";
            }
            
            if(Port == default(int))
            {
                Port = 10084;
            }
        }
    }
}