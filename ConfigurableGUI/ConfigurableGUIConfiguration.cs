using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace ConfigurableGUI
{
    public class ConfigurableGUIConfiguration
    {
        private const string relConfigFileLocation = @"..\..\..\ConfigurableGUI\bin\Debug";
        private const string configFileName = @"ConfigurableGUI.config.xml";

        private object configFileLock = new object();

        public string GreetingText;

        public void LoadFromConfig()
        {
            ConfigurableGUIConfiguration config;
            lock (configFileLock)
            {
                using (FileStream configFile = new FileStream(getFileLocationName(), FileMode.Open))
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
                using (StreamWriter configFile = new StreamWriter(getFileLocationName()))
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

        private string getFileLocationName()
        {
            var currentPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fullPath = Path.Combine(currentPath, relConfigFileLocation);
            return Path.Combine(fullPath, configFileName);
        }
    }
}
