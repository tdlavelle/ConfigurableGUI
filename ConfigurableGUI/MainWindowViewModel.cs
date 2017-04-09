using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Serialization;

namespace ConfigurableGUI
{
    class MainWindowViewModel : ObservableObject
    {
        #region Model
        // reference the model object here
        private readonly ConfigurableGUIConfiguration _config = new ConfigurableGUIConfiguration();
        #endregion Model

        #region Properties
        // private fields for each value to display
        private string _greetingText;

        // public properties for each value to display
        public string GreetingText
        {
            get { return _greetingText; }
            set
            {
                _greetingText = value;
                RaisePropertyChangedEvent("GreetingText");
            }
        }
        #endregion Properties

        #region Commands
        // public ICommands for each command able to be sent
        public ICommand LoadGreetingCommand
        {
            get { return new DelegateCommand(LoadGreetingTextFromConfig); }
        }

        public ICommand SaveGreetingCommand
        {
            get { return new DelegateCommand(SaveGreetingTextToConfig); }
        }
        #endregion Commands

        #region Helper Methods
        private void LoadGreetingTextFromConfig()
        {
            _config.LoadFromConfig();
            GreetingText = _config.GreetingText;
        }

        private void SaveGreetingTextToConfig()
        {
            _config.GreetingText = GreetingText;
            _config.SaveToConfig();
        }
        #endregion Helper Methods
    }
}
