using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ConfigurableGUI
{
    public class ConfigurableGUIConfiguration
    {
        private const string configFileLocation = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug\ConfigurableGUI.config.xml";
        private object configFileLock = new object();

        public string GreetingText;

        public void LoadFromConfig()
        {
            ConfigurableGUIConfiguration config;
            lock (configFileLock)
            {
                using (FileStream configFile = new FileStream(configFileLocation, FileMode.Open))
                {
                    XmlSerializer configLoader = createConfigLoader();
                    config = (ConfigurableGUIConfiguration)configLoader.Deserialize(configFile);
                }
            }

            GreetingText = config.GreetingText;
        }

        public void SaveToConfig()
        {
            lock (configFileLock)
            {
                using (StreamWriter configFile = new StreamWriter(configFileLocation))
                {
                    XmlSerializer configLoader = createConfigLoader();
                    configLoader.Serialize(configFile, this);
                }
            }
        }

        private static XmlSerializer createConfigLoader()
        {
            return new XmlSerializer(typeof(ConfigurableGUIConfiguration));
        }

    }
}
