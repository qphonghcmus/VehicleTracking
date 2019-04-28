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

        [DataMember]
        public string SqlServer { get; set; }

        [DataMember]
        public string SqlDbName { get; set; }

        [DataMember]
        public string SqlUser { get; set; }

        [DataMember]
        public string SqlPwd { get; set; }

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

            if (string.IsNullOrEmpty(SqlServer))
                SqlServer = @".\";

            if (string.IsNullOrEmpty(SqlDbName))
                SqlDbName = "VehicleData";

            if (string.IsNullOrEmpty(SqlUser))
                SqlUser = "sa";

            if (string.IsNullOrEmpty(SqlPwd))
                SqlPwd = "123456";
        }
    }
}