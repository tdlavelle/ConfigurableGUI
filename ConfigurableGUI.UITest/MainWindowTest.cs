using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using System.IO;
using System.Reflection;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.ScreenObjects;
using TestStack.White.ScreenObjects.Services;
using TestStack.White.ScreenObjects.Sessions;
using ConfigurableGUI.UITest.Screens;
using System.Threading;
using TestStack.White;

namespace ConfigurableGUI.UITest
{
    [TestFixture]
    public class MainWindowTest
    {
        // This is recreated for each test and the main fixture for each test to get its screen from
        private ScreenRepository theScreenRepo { get; set; }

        // This is recreated for each test and is only persisted in order to be able to call dispose on it in the teardown
        private WorkSession theWorkSession { get; set; }

        [SetUp]
        public void createApplication()
        {
            var directoryName = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var markpadLocation = Path.Combine(directoryName, @"ConfigurableGUI.exe");
            Application Application = Application.Launch(markpadLocation);

            var workConfiguration = new WorkConfiguration
            {
                ArchiveLocation = directoryName,
                Name = "ConfigurableGUI"
            };

            CoreAppXmlConfiguration.Instance.WorkSessionLocation = new DirectoryInfo(directoryName);
            theWorkSession = new WorkSession(workConfiguration, new NullWorkEnvironment());
            theScreenRepo = theWorkSession.Attach(Application);
        }

        [TearDown]
        public void destroyApplication()
        {
            theWorkSession.Dispose();
        }

        [Test]
        public void verifyGreetingLabel()
        {
            var mainWindow = theScreenRepo.Get<MainWindowScreen>("MainWindow", InitializeOption.NoCache);

            Assert.That(mainWindow.GreetingLabel.Equals("Greeting"));
        }

        [Test]
        public void verifySaveLoad()
        {
            var mainWindow = theScreenRepo.Get<MainWindowScreen>("MainWindow", InitializeOption.NoCache);

            mainWindow.GreetingText = "verifySaveLoadTest";
            mainWindow.Save();

            mainWindow.GreetingText = " ";
            mainWindow.Load();

            Assert.That(mainWindow.GreetingText.Equals("verifySaveLoadTest"));
        }
    }
}
