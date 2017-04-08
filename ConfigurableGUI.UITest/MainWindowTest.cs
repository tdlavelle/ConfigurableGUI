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
        [Test]
        public void verifyGreetingLabel()
        {
            var directoryName = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var markpadLocation = Path.Combine(directoryName, @"ConfigurableGUI.exe");
            Application Application = Application.Launch(markpadLocation);

            var workPath = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var workConfiguration = new WorkConfiguration
            {
                ArchiveLocation = workPath,
                Name = "ConfigurableGUI"
            };

            CoreAppXmlConfiguration.Instance.WorkSessionLocation = new DirectoryInfo(workPath);
            using (var workSession = new WorkSession(workConfiguration, new NullWorkEnvironment()))
            {
                var screenRepository = workSession.Attach(Application);
                var mainWindow = screenRepository.Get<MainWindowScreen>("MainWindow", InitializeOption.NoCache);

                Assert.That(mainWindow.GreetingLabel.Equals("Greeting"));
            }

            Application.Kill();
        }

        [Test]
        public void verifySaveLoad()
        {
            var directoryName = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var markpadLocation = Path.Combine(directoryName, @"ConfigurableGUI.exe");
            Application Application = Application.Launch(markpadLocation);

            var workPath = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var workConfiguration = new WorkConfiguration
            {
                ArchiveLocation = workPath,
                Name = "ConfigurableGUI"
            };

            CoreAppXmlConfiguration.Instance.WorkSessionLocation = new DirectoryInfo(workPath);
            using (var workSession = new WorkSession(workConfiguration, new NullWorkEnvironment()))
            {
                var screenRepository = workSession.Attach(Application);
                var mainWindow = screenRepository.Get<MainWindowScreen>("MainWindow", InitializeOption.NoCache);

                mainWindow.GreetingText = "verifySaveLoadTest";
                mainWindow.Save();

                mainWindow.GreetingText = " ";
                mainWindow.Load();

                Assert.That(mainWindow.GreetingText.Equals("verifySaveLoadTest"));

            }
            Application.Kill();

        }
    }
}
