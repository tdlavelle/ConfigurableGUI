using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace ConfigurableGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string configFileLocation = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug\ConfigurableGUI.config.xml";
        private object configFileLock = new object();
        public MainWindow()
        {
            InitializeComponent();
        }

        private string LoadGreetingTextFromConfig()
        {
            ConfigurableGUIConfiguration config;
            lock (configFileLock)
            {
                XmlSerializer configLoader = new XmlSerializer(typeof(ConfigurableGUIConfiguration));
                FileStream configFile = new FileStream(configFileLocation, FileMode.Open);
                config = (ConfigurableGUIConfiguration)configLoader.Deserialize(configFile);

                configFile.Close();
            }
            return config.GreetingText;
        }

        private void SaveGreetingTextToConfig(string greetingText)
        {
            ConfigurableGUIConfiguration config = new ConfigurableGUIConfiguration();
            config.GreetingText = greetingText;

            lock (configFileLock)
            {
                XmlSerializer configLoader = new XmlSerializer(typeof(ConfigurableGUIConfiguration));
                StreamWriter configFile = new StreamWriter(configFileLocation);
                configLoader.Serialize(configFile, config);
                configFile.Close();
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveGreetingTextToConfig(greetingText.Text);
        }

        private void loadButton_Click(object sender, RoutedEventArgs e)
        {
            greetingText.Text = LoadGreetingTextFromConfig();
        }
    }
}
