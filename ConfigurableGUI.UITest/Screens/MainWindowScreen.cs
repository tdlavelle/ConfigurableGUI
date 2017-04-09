using TestStack.White.ScreenObjects;
using TestStack.White.UIItems;
using TestStack.White.UIItems.WindowItems;

namespace ConfigurableGUI.UITest.Screens
{
    public class MainWindowScreen : Screen
    {
        protected Button SaveButton;
        protected Button LoadButton;

        public MainWindowScreen(Window window, ScreenRepository screenRepository) : base(window, screenRepository)
        {
        }

        public virtual string GreetingLabel
        {
            get { return Window.Get<Label>("GreetingLabel").Text; }
            set {}
        }

        public virtual string GreetingText
        {
            get { return Window.Get<TextBox>("GreetingText").Text; }
            set { Window.Get<TextBox>("GreetingText").Text = value; }
        }

        public virtual void Save()
        {
            SaveButton.Click();
            WaitWhileBusy();
        }

        public virtual void Load()
        {
            LoadButton.Click();
            WaitWhileBusy();
        }
    }
}