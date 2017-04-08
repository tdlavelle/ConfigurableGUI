using System;
using System.IO;
using System.Reflection;
using TestStack.White;

namespace ConfigurableGUI.UITest.Screens
{
    public abstract class UITestBase : IDisposable
    {
        public Application Application { get; private set; }

        protected UITestBase()
        {
            var directoryName = @"H:\Users\Thomas\Documents\Visual Studio 2015\Projects\ConfigurableGUI\ConfigurableGUI\bin\Debug";
            var markpadLocation = Path.Combine(directoryName, @"ConfigurableGUI.exe");
            Application = Application.Launch(markpadLocation);
        }

        public void Dispose()
        {
            Application.Dispose();
        }
    }
}