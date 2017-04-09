using NUnit.Framework;
using System.IO;
using System.Reflection;
using TestStack.White.Configuration;
using TestStack.White.Factory;
using TestStack.White.ScreenObjects;
using TestStack.White.ScreenObjects.Services;
using TestStack.White.ScreenObjects.Sessions;
using ConfigurableGUI.UITest.Screens;
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
            var relPath = @"..\..\..\ConfigurableGUI\bin\Debug";
            var currentPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var fullPath = Path.Combine(currentPath, relPath);
            var appName = @"ConfigurableGUI.exe";
            var markpadLocation = Path.Combine(fullPath, appName);
            Application Application = Application.Launch(markpadLocation);

            var workConfiguration = new WorkConfiguration
            {
                ArchiveLocation = fullPath,
                Name = "ConfigurableGUI"
            };

            CoreAppXmlConfiguration.Instance.WorkSessionLocation = new DirectoryInfo(fullPath);
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
