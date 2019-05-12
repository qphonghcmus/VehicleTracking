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
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class GlobalVar 
    {
        private IConfigManager configManager;

        public ConfigObject configObject { get; set; }

        private const string path = "Config.xml";

        public GlobalVar()
        {
            configManager = new ConfigManager();
            configObject = new ConfigObject();
            LoadConfigFromFile();
        }

        public void LoadConfigFromFile()
        {
            try
            {
                configObject = configManager.Read<ConfigObject>(HostingEnvironment.MapPath("~/bin/") + path);
            }
            catch
            {
            }
        }

    }
}