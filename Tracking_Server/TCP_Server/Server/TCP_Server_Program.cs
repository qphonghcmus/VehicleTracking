using ConfigFile;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using TCP_Server.Controllers.Config;

namespace TCP_Server.Server
{
    public class TCP_Server_Program
    {
        private static TcpServer _server;
        private static string IpAddress = null;
        private static int PortNumber = 0;

        // Log
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static void Main()
        {
            _server = new TcpServer();
            LoadConfig();
            _server.CreateServer(IpAddress, PortNumber);
            _server.StartServer();

        }

        static void LoadConfig()
        {
            try
            {
                GlobalVar global = new GlobalVar();
                IpAddress = global.configObject.IpAddress;
                PortNumber = global.configObject.Port;

                log.Info("Config successfully");
            }catch(Exception ex)
            {
                log.Error("Error when loading config file", ex);
            }
        }

        static void SaveConfig()
        {
            IConfigManager configManager = new ConfigManager();
            ConfigObject configObject = new ConfigObject();
            configObject.Fix();
            configManager.Write<ConfigObject>(configObject, HostingEnvironment.MapPath("~/bin/") + "Config.xml");
        }

    }
}